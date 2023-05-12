using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using server.Models;
using server.Persistence;
using server.Services;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    public AuthController(IPasswordService passwordService, IApplicationDbContext context, IJwtService jwtService, IHttpContextAccessor httpContext)
    {
        _passwordService = passwordService;
        _jwtService = jwtService;
        _context = context;
        _httpContext = httpContext; 
    }   

    [HttpPost("login")]
    public async Task<ActionResult> LoginPost(LoginDto login)
    {
        try
        {
           var user = await _context.User.Where(x => x.Username == login.Username).FirstOrDefaultAsync();

           if(user == null || !_passwordService.VerifyPassword(login.Password, user.Password))
           {
                return Unauthorized(new 
                {
                    errorMessage = "Invalid Username or Password."
                });
           }

            var mapped = new AuthDetailsDto
            {
                Id = user.Id,
                Username = user.Username,
                AccessToken = _jwtService.GenerateJwt(user.Id, false) 
            };
            string refreshToken = _jwtService.GenerateJwt(user.Id, true);
            Response.Cookies.Append("rt", refreshToken, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7),
                HttpOnly = true
            });
            return Ok(mapped);
        }
        catch (Exception ex)
        {
            return BadRequest(new 
            {
                errorMessage = ex.Message
            }); 
        }
    }

    [HttpGet("refresh")]
    public async Task<ActionResult> RefreshToken()
    {
        try
        {
            var oldToken = Request.Cookies["rt"];
            if(string.IsNullOrEmpty(oldToken))
            {
                return NotFound(new 
                {
                    errorMessage = "Refresh token was not found"
                });
            }
            var userId = _jwtService.VerifyRefreshToken(oldToken);
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id.ToString() == userId);
            if(user == null)
            {
                return Unauthorized(new 
                {
                    errorMessage = "User not found."
                });
            }
            var mapped = new AuthDetailsDto
            {
                Id = user.Id,
                Username = user.Username,
                AccessToken = _jwtService.GenerateJwt(user.Id, false) 
            };
            string refreshToken = _jwtService.GenerateJwt(user.Id, true);
            Response.Cookies.Append("rt", refreshToken, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7),
                HttpOnly = true
            });
            return Ok(mapped);
        }
        catch (Exception ex)
        {
            return BadRequest(new 
            {
                errorMessage = ex.Message
            }); 
        }
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            if(_httpContext.HttpContext == null)
                return NotFound(new 
                {
                    errorMessage = "httpContext not found"
                });

            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var user = await _context.User.Select(p => new {
                p.Id,
                p.Username,
            }).FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            if(user == null)
                return NotFound(new 
                {
                    errorMessage = "User not found"
                });

            return Ok(user);
        }
        catch(Exception ex)
        {
            return BadRequest(new 
            {
                errorMessage = ex.Message
            });
        }
    }
    [Authorize]
    [HttpPost("logout")]
    public IActionResult LogoutUser()
    {
        try
        {
            Response.Cookies.Delete("rt");
            return NoContent();

        }
        catch(Exception ex)
        {
            return BadRequest(new 
            {
                errorMessage = ex.Message
            });
        }
    }
} 
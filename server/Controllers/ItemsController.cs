using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Entities;
using server.Models;
using server.Persistence;

namespace server.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContext;
    public ItemsController(IApplicationDbContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext; 
    }

    [HttpGet]    
    public async Task<IActionResult> GetItems()
    {
        try
        {
            if(_httpContext.HttpContext == null)

                return NotFound(new 
                {
                    errorMessage = "httpContext not found"
                });

            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var results = await _context.Items
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.UserId,
                    p.CreatedAt,
                    p.UpdatedAt
                })
                .Where(x => x.UserId.ToString() == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.UpdatedAt)
                .ToListAsync();
                
            return Ok(results);
        }   
        catch(Exception ex)
        {
            return BadRequest(new 
            {
                errorMessage = ex.Message
            });
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddItems(AddItemsDto addItems)
    {
        try
        {

            if(_httpContext.HttpContext == null)

                return NotFound(new 
                {
                    errorMessage = "httpContext not found"
                });

            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var user = await _context.User.FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            if(user == null)
            {
                return NotFound(new 
                {
                    errorMessage = "User not found"
                });
            }

            var newItem = new Items
            {
                Name = addItems.Name
            };

            user.Items.Add(newItem);
            await _context.SaveChangesAsync();

            return Accepted();
        }
        catch(Exception ex)
        {
            return BadRequest(new 
            {
                errorMessage = ex.Message
            });
        }
    }
    
    [HttpDelete("{itemsId}")]
    public async Task<IActionResult> DeleteItem(string itemsId)
    {
        try
        {
            var items = await _context.Items.FirstOrDefaultAsync(x => x.Id.ToString() == itemsId);

            if(items == null)
            {
                return NotFound(new 
                {
                    errorMessage = "Item was not found."
                });
            }

            _context.Items.Remove(items);
            await _context.SaveChangesAsync();

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
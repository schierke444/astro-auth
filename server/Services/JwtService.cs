using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace server.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateJwt(Guid Id, bool isRefreshToken)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]!));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Id.ToString())
        };

        var tokenToWrite = new JwtSecurityToken
        (
            _config["Authentication:Issuer"],
            _config["Authentication:Audience"],
            claims,
            DateTime.Now,
            isRefreshToken ? DateTime.Now.AddDays(7) : DateTime.Now.AddSeconds(5),
            signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenToWrite);

        return token;
    }

    public string VerifyRefreshToken(string oldToken)
    {
        var decoded = new JwtSecurityTokenHandler().ValidateToken(
            oldToken,
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Authentication:Issuer"],
                ValidAudience = _config["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]!))
            },
            out SecurityToken validatedToken
        );

        if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new Exception("Invalid Token");
        }

        if(decoded.Identity == null ||  decoded.Identity.Name == null)
        {
            throw new Exception("Invalid Token and User");
        }

        return decoded.Identity.Name;
    }
}
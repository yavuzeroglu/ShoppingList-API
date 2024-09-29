using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingList.Application.Abstractions.Tokens;
using ShoppingList.Application.DTOs;

namespace ShoppingList.Infrastructure.Services.Token;

public class TokenService : ITokenService
{

   private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDTO CreateAccessToken(int minutes)
    {
        TokenDTO token = new();
        
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
 

        token.Expiration = DateTime.UtcNow.AddMinutes(minutes);
        JwtSecurityToken securityToken = new(
         audience: _configuration["JWT:Audience"],
         issuer: _configuration["JWT:Issuer"],
         expires: token.Expiration,
         notBefore: DateTime.UtcNow,
         signingCredentials: new(securityKey, SecurityAlgorithms.HmacSha256)
        );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}
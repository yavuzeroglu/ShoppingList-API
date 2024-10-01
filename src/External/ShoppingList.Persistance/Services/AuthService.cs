using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Abstractions.Services;
using ShoppingList.Application.Abstractions.Tokens;
using ShoppingList.Application.DTOs;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Persistance.Services;

public class AuthService : IAuthService
{
   private readonly UserManager<AppUser> _userManager;
   private readonly ITokenService _tokenService;
   private readonly IUserService _userService;

   public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, IUserService userService)
   {
      _userManager = userManager;
      _tokenService = tokenService;
      _userService = userService;
   }

   public async Task<TokenDTO> LoginAsync(string usernameOrEmail, string password, int accessTokenLifetime)
   {
      AppUser? user = await _userManager.FindByNameAsync(usernameOrEmail);
      if (user is null)
         user = await _userManager.FindByEmailAsync(usernameOrEmail);

      if (user is null)
         throw new Exception("Kullanici veya sifre hatali");

      bool checkPassword = await _userManager.CheckPasswordAsync(user, password);

      if (checkPassword)
      {
         TokenDTO token = _tokenService.CreateAccessToken(accessTokenLifetime);
         await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1);
         return new()
         {
            AccessToken = token.AccessToken,
            Expiration = token.Expiration,
            RefreshToken = token.RefreshToken
         };
      }
      throw new Exception("Kullanici adi veya sifre hatali");
   }

   public async Task<TokenDTO> RefreshTokenLogin(string refreshToken)
   {
      AppUser? user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
      if (user is not null && user?.RefreshTokenEndDate > DateTime.UtcNow)
      {
         TokenDTO token = _tokenService.CreateAccessToken(2);
         await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1);
         return token;
      } else 
         throw new Exception("Kullanici Bulunamadi");
   }
}
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.Common.Abstractions.Tokens;
using ShoppingList.Application.DTOs;
using ShoppingList.Application.Helpers;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Persistance.Services;

public class AuthService : IAuthService
{
   private readonly UserManager<AppUser> _userManager;
   private readonly ITokenService _tokenService;
   private readonly IUserService _userService;
   private readonly IMailService _mailService;


    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, IUserService userService, IMailService mailService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _userService = userService;
        _mailService = mailService;
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
         TokenDTO token = _tokenService.CreateAccessToken(accessTokenLifetime, user);
         await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 1);
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
         TokenDTO token = _tokenService.CreateAccessToken(2, user);
         await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 1);
         return token;
      }
      else
         throw new Exception("Kullanici Bulunamadi");
   }


   public async Task PasswordResetAsync(string email)
   {
      AppUser? user = await _userManager.FindByEmailAsync(email);
      if (user is not null)
      {
         string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
         resetToken = resetToken.UrlEncode();

         await _mailService.SendPasswordMailAsync(email, user.Id, resetToken);
      }
      else
         throw new Exception("Mail Kayıtlı Değil");
   }

   public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
   {
      AppUser? user = await _userManager.FindByIdAsync(userId);
      if (user is not null)
      {
         resetToken = resetToken.UrlDecode();

         return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
      }
      return false;
   }

}
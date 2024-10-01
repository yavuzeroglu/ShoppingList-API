using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Abstractions.Services;
using ShoppingList.Application.DTOs.Users;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Persistance.Services;

public class UserService : IUserService
{
   private readonly UserManager<AppUser> _userManager;

   public UserService(UserManager<AppUser> userManager)
   {
      _userManager = userManager;
   }

   public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
   {
      IdentityResult result = await _userManager.CreateAsync(new()
      {
         Id = Guid.NewGuid().ToString(),
         UserName = model.UserName,
         Email = model.Email
      }, model.Password);
      CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };

      if (result.Succeeded)
         response.Message = "Kullanici basariyla olusturulmustur.";
      else
      {
         foreach (var error in result.Errors)
         {
            response.Message += $"{error.Code} --- {error.Description} \n";
         }
      }

      return response;
   }

   public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken)
   {
      if (user is not null)
      {
         user.RefreshToken = refreshToken;
         user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessToken);
         await _userManager.UpdateAsync(user);
      }
      else 
         throw new Exception("Kullanici bulunamadi");


   }
}
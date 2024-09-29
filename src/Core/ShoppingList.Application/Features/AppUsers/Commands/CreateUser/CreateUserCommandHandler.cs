using System.Reflection.Metadata;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Features.AppUsers.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
   private readonly UserManager<AppUser> _userManager;

   public CreateUserCommandHandler(UserManager<AppUser> userManager)
   {
      _userManager = userManager;
   }

   public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
   {
      IdentityResult result = await _userManager.CreateAsync(new()
      {
         Id = Guid.NewGuid().ToString(),
         UserName = request.UserName,
         Email = request.Email
      }, request.Password);
      CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

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
}
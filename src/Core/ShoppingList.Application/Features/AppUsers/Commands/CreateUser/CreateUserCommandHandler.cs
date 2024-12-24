using System.Reflection.Metadata;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Users;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Features.AppUsers.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
   private readonly IUserService _userService;

   public CreateUserCommandHandler(IUserService userService)
   {
      _userService = userService;
   }

   public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
   {
      CreateUserResponseDTO response = await _userService.CreateAsync(new()
      {
         UserName = request.UserName,
         Email = request.Email,
         Password = request.Password,
         PasswordConfirm = request.PasswordConfirm
      });

      return new()
      {
         Message = response.Message,
         Succeeded = response.Succeeded
      };
   }
}
using MediatR;

namespace ShoppingList.Application.Features.AppUsers.Commands.CreateUser;

public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
{
   public string UserName { get; set; } = string.Empty; 
   public string Email { get; set; } = string.Empty; 
   public string Password { get; set; } = string.Empty; 
   public string PasswordConfirm { get; set; } = string.Empty; 

}

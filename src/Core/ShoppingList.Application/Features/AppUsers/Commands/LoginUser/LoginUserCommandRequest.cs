using MediatR;

namespace ShoppingList.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
   public string UsernameOrEmail { get; set; } = string.Empty;
   public string Password { get; set; } = string.Empty;
}


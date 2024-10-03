using MediatR;

namespace ShoppingList.Application.Features.AppUsers.Commands.PasswordReset;

public class PasswordResetCommandRequest : IRequest<Unit>
{
   public string Email { get; set; }
}
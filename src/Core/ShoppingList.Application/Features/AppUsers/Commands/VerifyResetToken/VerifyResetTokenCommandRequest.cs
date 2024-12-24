using MediatR;

namespace ShoppingList.Application.Features.AppUsers.Commands.VerifyResetToken;

public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
{
   public string ResetToken { get; set; } = string.Empty;
   public string UserId { get; set; } = string.Empty;
}

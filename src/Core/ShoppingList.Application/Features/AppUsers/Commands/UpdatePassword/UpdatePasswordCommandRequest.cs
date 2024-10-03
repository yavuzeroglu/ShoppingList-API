using MediatR;

namespace ShoppingList.Application.Features.AppUsers.Commands.UpdatePassword;

public record UpdatePasswordCommandRequest : IRequest<Unit>
{
    public string UserId { get; init; } = string.Empty;
    public string ResetToken { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string PasswordConfirm { get; init; } = string.Empty;
}

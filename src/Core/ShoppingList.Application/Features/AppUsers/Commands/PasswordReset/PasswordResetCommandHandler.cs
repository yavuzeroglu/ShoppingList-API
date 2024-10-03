using MediatR;
using ShoppingList.Application.Abstractions.Services;

namespace ShoppingList.Application.Features.AppUsers.Commands.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, Unit>
{
    private readonly IAuthService _authService;

    public PasswordResetCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Unit> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
    {
        await _authService.PasswordResetAsync(request.Email);
        return Unit.Value;
    }
}

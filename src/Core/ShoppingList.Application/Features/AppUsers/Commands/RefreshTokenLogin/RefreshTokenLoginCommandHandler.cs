using MediatR;
using ShoppingList.Application.Abstractions.Services;
using ShoppingList.Application.DTOs;

namespace ShoppingList.Application.Features.AppUsers.Commands.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
    {
        TokenDTO token = await _authService.RefreshTokenLogin(request.RefreshToken);
        return new()
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken
        };
    }
}
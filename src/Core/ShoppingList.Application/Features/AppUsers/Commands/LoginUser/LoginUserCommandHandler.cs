using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {

        TokenDTO token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 60);

        return new LoginUserCommandResponse()
        {
            Token = token.AccessToken,
            Expiration = token.Expiration,
            RefreshToken = token.RefreshToken
        };
    }
}


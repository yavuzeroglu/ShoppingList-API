using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Abstractions.Tokens;
using ShoppingList.Application.DTOs;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    public LoginUserCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
        if (user is null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user is null)
            throw new Exception("Kullanici veya sifre hatali");

        bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (checkPassword)
        {
            TokenDTO token = _tokenService.CreateAccessToken(5);
            return new()
            {
                Token = token.AccessToken,
                Expiration = token.Expiration
            };
        }

        throw new Exception("Kullanici adi veya sifre hatali");

    }
}


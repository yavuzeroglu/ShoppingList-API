using ShoppingList.Application.DTOs;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Abstractions.Tokens;

public interface ITokenService
{
    TokenDTO CreateAccessToken(int minutes, AppUser user);
    string CreateRefreshToken();

}

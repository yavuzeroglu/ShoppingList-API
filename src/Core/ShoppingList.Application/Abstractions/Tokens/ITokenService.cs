using ShoppingList.Application.DTOs;

namespace ShoppingList.Application.Abstractions.Tokens;

public interface ITokenService
{
    TokenDTO CreateAccessToken(int minutes);
}

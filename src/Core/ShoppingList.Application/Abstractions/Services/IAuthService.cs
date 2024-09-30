using ShoppingList.Application.DTOs;

namespace ShoppingList.Application.Abstractions.Services;

public interface IAuthService
{
   Task<TokenDTO> LoginAsync(string usernameOrEmail, string password, int accessTokenLifetime);
}
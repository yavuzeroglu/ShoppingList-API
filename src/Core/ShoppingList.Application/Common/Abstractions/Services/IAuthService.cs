using ShoppingList.Application.DTOs;

namespace ShoppingList.Application.Common.Abstractions.Services;

public interface IAuthService
{
   Task<TokenDTO> LoginAsync(string usernameOrEmail, string password, int accessTokenLifetime);
   Task<TokenDTO> RefreshTokenLogin(string refreshToken);
   Task PasswordResetAsync(string email);
   Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
}
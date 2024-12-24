using ShoppingList.Application.DTOs.Users;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Common.Abstractions.Services;

public interface IUserService
{
   Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);
   Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken);
   Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
}
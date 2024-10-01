using ShoppingList.Application.DTOs.Users;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Abstractions.Services;

public interface IUserService
{
   Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);
   Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessToken);
}
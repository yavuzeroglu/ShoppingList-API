using ShoppingList.Domain.Entities;
using ShoppingList.Domain.Enums;

namespace ShoppingList.Application.Common.Abstractions.Services;

public interface IBasketUserService
{
    // Queries
    Task<List<BasketUser>> GetBasketUsersAsync(int basketId);
    Task<bool> IsUserInBasketAsync(int basketId, string userId);
    Task<BasketUserRole> GetUserRoleInBasketAsync(int basketId, string userId);

    // Commands
    Task<BasketUser> AddUserToBasketAsync(int basketId, string userId, BasketUserRole role);
    Task RemoveUserFromBasketAsync(int basketId, string userId);
    Task UpdateUserRoleAsync(int basketId, string userId, BasketUserRole newRole);
}


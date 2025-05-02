using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Common.Abstractions.Services;

public interface IBasketItemService
{
    // Queries
    Task<BasketItem> GetBasketItemByIdAsync(int basketItemId);
    Task<List<BasketItemDTO>> GetBasketItemsAsync(int basketId);
    Task<bool> IsBasketItemExistsAsync(int basketItemId);
    Task<decimal> GetBasketTotalAsync(int basketId);

    // Commands
    Task<BasketItem> AddItemToBasketAsync(int basketId, int productId, int quantity);
    Task RemoveBasketItemAsync(int basketItemId);
    Task UpdateQuantityAsync(int basketItemId, int quantity);
    Task TogglePurchaseStatusAsync(int basketItemId);
    Task AssignUserToBasketItemAsync(int basketItemId, string userId);
    Task RemoveUserFromBasketItemAsync(int basketItemId);
}
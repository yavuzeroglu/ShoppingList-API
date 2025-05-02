using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Common.Abstractions.Services;

public interface IBasketService
{
    // Queries
    Task<List<Basket>> GetUserBasketsAsync();
    Task<Basket> GetBasketByIdAsync(int basketId);
    Task<bool> IsBasketExistsAsync(int basketId);
    Task<bool> HasUserAccessToBasket(int basketId);
    
    // Commands
    Task<Basket> CreateBasketAsync(string basketName);
    Task UpdateBasketAsync(int basketId, string newName);
    Task RemoveBasketAsync(int basketId);
    Task<Basket> DuplicateBasketAsync(int basketId, string newBasketName);
    Task UpdatePlannedPurchaseDateAsync(int basketId, DateTime? plannedPurchaseDate);
}
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Common.Abstractions.Services;

public interface IBasketService
{

  // Queries
  public Task<List<Basket>> GetBasketsAsync();
  public Task<Basket> GetBasketByIdAsync(int basketId);
  public Task<bool> IsBasketEmptyAsync(int basketId);
  public Task<BasketItem> GetBasketItemByIdAsync(int basketItemId);
  public Task<bool> IsBasketItemExistsAsync(int basketItemId);
  public Task<int> GetTotalBasketCountAsync(); 

  // Commands
  public Task CreateBasketAsync(string BasketName);
  public Task AddItemToBasketAsync(int basketId, int productId, int quantity);
  public Task RemoveBasketItemAsync(int basketItemId);
  public Task UpdateQuantityBasketItemAsync(int basketItemId, UpdateQuantityDTO dto);
  public Task RemoveBasketAsync(int basketId);
  public Task DuplicateBasketAsync(int basketId, string newBasketName); 
  public Task SwitchPurchaseBasketItemAsync(int basketItemId);
  
  
  
}
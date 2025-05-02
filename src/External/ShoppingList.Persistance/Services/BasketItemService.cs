using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;
using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Services;

public class BasketItemService : IBasketItemService
{
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IBasketService _basketService;
    private readonly ShoppingListDbContext _context;

    public BasketItemService(
        IBasketItemReadRepository basketItemReadRepository,
        IBasketItemWriteRepository basketItemWriteRepository,
        IBasketReadRepository basketReadRepository,
        IProductReadRepository productReadRepository,
        IBasketService basketService,
        ShoppingListDbContext context)
    {
        _basketItemReadRepository = basketItemReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketReadRepository = basketReadRepository;
        _productReadRepository = productReadRepository;
        _basketService = basketService;
        _context = context;
    }

    // Queries
    public async Task<BasketItem> GetBasketItemByIdAsync(int basketItemId)
    {
        var basketItem = await _basketItemReadRepository
            .GetWhere(x => x.Id == basketItemId)
            .Include(bi => bi.Product)
            .FirstOrDefaultAsync();

        if (basketItem == null)
            throw new NotFoundException("Basket item not found");

        // Check if user has access to the basket
        if (!await _basketService.HasUserAccessToBasket(basketItem.BasketId))
            throw new UnauthorizedException();

        return basketItem;
    }

    public async Task<List<BasketItemDTO>> GetBasketItemsAsync(int basketId)
    {
        if (!await _basketService.HasUserAccessToBasket(basketId))
            throw new UnauthorizedException();

        var basketItems = await _basketItemReadRepository
            .GetWhere(x => x.BasketId == basketId)
            .Include(bi => bi.Product)
            .Include(bi => bi.AssignedUser)
            .ToListAsync();

        return basketItems.Select(x => new BasketItemDTO
        {
            Id = x.Id,
            BasketId = x.BasketId,
            ProductId = x.ProductId,
            ProductName = x.Product?.Name ?? string.Empty,
            Quantity = x.Quantity,
            LineTotal = x.LineTotal,
            AssignedUserId = x.AssignedUserId,
            AssignedUserName = x.AssignedUser?.UserName,
            IsPurchased = x.IsPurchased,
            CreatedDate = x.CreatedDate,
            ModifiedDate = x.ModifiedDate
        }).ToList();
    }

    public async Task<bool> IsBasketItemExistsAsync(int basketItemId)
    {
        return await _basketItemReadRepository.GetByIdAsync(basketItemId) != null;
    }

    public async Task<decimal> GetBasketTotalAsync(int basketId)
    {
        if (!await _basketService.HasUserAccessToBasket(basketId))
            throw new UnauthorizedException();

        return await _basketItemReadRepository
            .GetWhere(x => x.BasketId == basketId)
            .SumAsync(x => x.LineTotal);
    }

    // Commands
    public async Task<BasketItem> AddItemToBasketAsync(int basketId, int productId, int quantity)
    {
        if (!await _basketService.HasUserAccessToBasket(basketId))
            throw new UnauthorizedException();

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var product = await _productReadRepository.GetByIdAsync(productId)
                ?? throw new NotFoundException("Product not found");

            var existingItem = await _basketItemReadRepository
                .GetWhere(x => x.BasketId == basketId && x.ProductId == productId)
                .FirstOrDefaultAsync();

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.LineTotal = existingItem.Quantity * product.EstimatedPrice;
                _basketItemWriteRepository.Update(existingItem);
                await _basketItemWriteRepository.SaveAsync();
                await transaction.CommitAsync();
                return existingItem;
            }

            var newBasketItem = new BasketItem
            {
                BasketId = basketId,
                ProductId = productId,
                Quantity = quantity,
                LineTotal = quantity * product.EstimatedPrice
            };

            await _basketItemWriteRepository.AddAsync(newBasketItem);
            await _basketItemWriteRepository.SaveAsync();
            await transaction.CommitAsync();
            return newBasketItem;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RemoveBasketItemAsync(int basketItemId)
    {
        var basketItem = await GetBasketItemByIdAsync(basketItemId);

        if (!await _basketService.HasUserAccessToBasket(basketItem.BasketId))
            throw new UnauthorizedException();

        _basketItemWriteRepository.Remove(basketItem);
        await _basketItemWriteRepository.SaveAsync();
    }

    public async Task UpdateQuantityAsync(int basketItemId, int quantity)
    {
        if (quantity < 1)
            throw new ArgumentException("Quantity must be greater than zero");

        var basketItem = await GetBasketItemByIdAsync(basketItemId);

        if (!await _basketService.HasUserAccessToBasket(basketItem.BasketId))
            throw new UnauthorizedException();

        var product = await _productReadRepository.GetByIdAsync(basketItem.ProductId)
            ?? throw new NotFoundException("Product not found");

        basketItem.Quantity = quantity;
        basketItem.LineTotal = quantity * product.EstimatedPrice;

        _basketItemWriteRepository.Update(basketItem);
        await _basketItemWriteRepository.SaveAsync();
    }

    public async Task TogglePurchaseStatusAsync(int basketItemId)
    {
        var basketItem = await GetBasketItemByIdAsync(basketItemId);

        if (!await _basketService.HasUserAccessToBasket(basketItem.BasketId))
            throw new UnauthorizedException();

        basketItem.IsPurchased = !basketItem.IsPurchased;

        _basketItemWriteRepository.Update(basketItem);
        await _basketItemWriteRepository.SaveAsync();
    }

    public async Task AssignUserToBasketItemAsync(int basketItemId, string userId)
    {
        var basketItem = await GetBasketItemByIdAsync(basketItemId);

        if (!await _basketService.HasUserAccessToBasket(basketItem.BasketId))
            throw new UnauthorizedException();

        basketItem.AssignedUserId = userId;

        _basketItemWriteRepository.Update(basketItem);
        await _basketItemWriteRepository.SaveAsync();
    }

    public async Task RemoveUserFromBasketItemAsync(int basketItemId)
    {
        var basketItem = await GetBasketItemByIdAsync(basketItemId);

        if (!await _basketService.HasUserAccessToBasket(basketItem.BasketId))
            throw new UnauthorizedException();

        basketItem.AssignedUserId = null;

        _basketItemWriteRepository.Update(basketItem);
        await _basketItemWriteRepository.SaveAsync();
    }
}
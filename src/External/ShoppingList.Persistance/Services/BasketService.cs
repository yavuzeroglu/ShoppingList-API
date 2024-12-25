using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;
using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;
using ShoppingList.Domain.Entities.Identity;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Services;

public class BasketService : IBasketService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly ShoppingListDbContext _context;
    private readonly IMapper _mapper;

    public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IBasketReadRepository basketReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IMapper mapper, ShoppingListDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _basketReadRepository = basketReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _mapper = mapper;
        _context = context;
    }

    // Commands
    public async Task CreateBasketAsync(string BasketName)
    {
        AppUser? user = await IsUserExistsAsync();

        Basket basket = new()
        {
            CreatedByUserId = user?.Id,
            BasketName = BasketName,
            IsPurchased = false,
            TotalAmount = 0,
            CreatedDate = DateTime.UtcNow
        };

        await _basketWriteRepository.AddAsync(basket);
        await _basketWriteRepository.SaveAsync();

    }

    public async Task AddItemToBasketAsync(int basketId, int productId, int quantity, decimal unitPrice)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await IsUserExistsAsync();
            Basket basket = await _basketReadRepository
                .GetSingleAsync(x => x.Id == basketId && x.CreatedByUserId == user.Id,
                                x => x.Include(b => b.BasketItems).ThenInclude(bi => bi.Product))
               ?? throw new NotFoundException("Basket Not Found");

            BasketItem? basketItem = basket
                                .BasketItems
                                .FirstOrDefault(x => x.ProductId == productId);
            if (basketItem is not null)
            {
                basketItem.Quantity += quantity;
                basketItem.LineTotal = basketItem.Quantity * unitPrice;
                UpdateQuantityDTO updateQuantityDTO = new()
                {
                    Quantity = basketItem.Quantity,
                    UnitPrice = unitPrice
                };
                await UpdateQuantityBasketItemAsync(basketItem.Id, updateQuantityDTO);

                _basketItemWriteRepository.Update(basketItem);
            }
            else
            {
                BasketItem newBasketItem = new()
                {
                    BasketId = basketId,
                    ProductId = productId,
                    Quantity = quantity,
                    LineTotal = quantity * unitPrice
                };

                await _basketItemWriteRepository.AddAsync(newBasketItem);
            }
            basket.TotalAmount = basket.BasketItems.Sum(b => b.LineTotal);
            await _basketItemWriteRepository.SaveAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RemoveBasketItemAsync(int basketItemId)
    {
        if (!await IsBasketItemExistsAsync(basketItemId))
            throw new NotFoundException("Basket Item not found");

        BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        Basket basket = await GetBasketByIdAsync(basketItem.BasketId);

        basket.TotalAmount -= basketItem.LineTotal;
        _basketItemWriteRepository.Remove(basketItem);
        await _basketItemWriteRepository.SaveAsync();
    }

    public async Task UpdateQuantityBasketItemAsync(int basketItemId, UpdateQuantityDTO dto)
    {
        BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        Basket basket = await _basketReadRepository.GetByIdAsync(basketItem.BasketId);

        basket.TotalAmount -= basketItem.LineTotal;
        basketItem.Quantity = dto.Quantity;
        basketItem.LineTotal = dto.Quantity * dto.UnitPrice;
        basket.TotalAmount += basketItem.LineTotal;
        await _basketWriteRepository.SaveAsync();
    }

    public async Task RemoveBasketAsync(int basketId)
    {
        Basket basket = await _basketReadRepository.GetByIdAsync(basketId)
            ?? throw new NotFoundException("Basket Not Found");

        if (basket.BasketItems.Any())
        {
            throw new InvalidOperationException("Cannot delete a basket that has items.");
        }

        _basketWriteRepository.Remove(basket);
        await _basketWriteRepository.SaveAsync();
    }

    public async Task DuplicateBasketAsync(int basketId, string newBasketName)
    {
        var sourceBasket = await GetBasketByIdAsync(basketId);
        
        Basket newBasket = new()
        {
            CreatedByUserId = sourceBasket.CreatedByUserId,
            BasketName = newBasketName,
            IsPurchased = false,
            TotalAmount = 0,
            CreatedDate = DateTime.UtcNow
        };

        await _basketWriteRepository.AddAsync(newBasket);
        await _basketWriteRepository.SaveAsync();

        foreach (var item in sourceBasket.BasketItems)
        {
            await AddItemToBasketAsync(
                newBasket.Id,
                item.ProductId,
                item.Quantity,
                item.LineTotal / item.Quantity // Birim fiyatı hesapla
            );
        }
    }

    // Queries
    public async Task<List<Basket>> GetBasketsAsync()
    {
        AppUser? user = await IsUserExistsAsync();

        var baskets = await _basketReadRepository
            .GetWhere(x => x.CreatedByUserId.Equals(user.Id))
            .Select(b => new Basket
            {
                Id = b.Id,
                CreatedByUserId = b.CreatedByUserId,
                BasketName = b.BasketName,
                IsPurchased = b.IsPurchased,
                TotalAmount = b.BasketItems.Sum(bi => bi.LineTotal),
                BasketItems = b.BasketItems.Select(bi => new BasketItem
                {
                    Id = bi.Id,
                    ProductId = bi.ProductId,
                    Product = bi.Product,
                    Quantity = bi.Quantity,
                    LineTotal = bi.LineTotal
                }).ToList()
            })
            .ToListAsync();

        return baskets;
    }

    public async Task<Basket> GetBasketByIdAsync(int basketId)
    {
        AppUser? user = await IsUserExistsAsync();

        Basket? basket = await _basketReadRepository
            .GetWhere(x => x.CreatedByUserId.Equals(user.Id) && x.Id.Equals(basketId),
                      x => x.Include(b => b.BasketItems)
                            .ThenInclude(bi => bi.Product))
            .FirstOrDefaultAsync();
        if (basket is null)
            throw new NotFoundException("Basket not found");

        return basket;
    }

    public async Task<BasketItem> GetBasketItemByIdAsync(int basketItemId)
    {
        BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        if (basketItem is null)
            throw new NotFoundException("Basket Item not found");
        return basketItem;
    }

    public async Task<bool> IsBasketItemExistsAsync(int basketItemId)
    {
        BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        return basketItem is not null;
    }

    public async Task<bool> IsBasketEmptyAsync(int basketId)
    {
        var basket = await GetBasketByIdAsync(basketId);
        return !basket.BasketItems.Any();
    }

    public async Task<int> GetTotalBasketCountAsync()
    {
        AppUser? user = await IsUserExistsAsync();
        
        return await _basketReadRepository
            .GetWhere(x => x.CreatedByUserId.Equals(user.Id))
            .CountAsync();
    }


    // Private Methods
    private async Task<AppUser> IsUserExistsAsync()
    {
        string? userName = _httpContextAccessor?
            .HttpContext
            .User
            .FindFirstValue(ClaimTypes.Name);

        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName)
            ?? throw new UnauthorizedException();

        return user;
    }

}

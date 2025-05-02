using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;
using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
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
    private readonly IProductReadRepository _productReadRepository;
    private readonly ShoppingListDbContext _context;
    private readonly IMapper _mapper;

    public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IBasketReadRepository basketReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IMapper mapper, ShoppingListDbContext context, IProductReadRepository productReadRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _basketReadRepository = basketReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _mapper = mapper;
        _context = context;
        _productReadRepository = productReadRepository;

    }

    // Commands
    public async Task<Basket> CreateBasketAsync(string basketName)
    {
        var user = await GetCurrentUserAsync();

        var basket = new Basket
        {
            CreatedByUserId = user.Id,
            Name = basketName,
            CreatedDate = DateTime.UtcNow
        };

        await _basketWriteRepository.AddAsync(basket);
        await _basketWriteRepository.SaveAsync();

        return basket;
    }

    public async Task AddItemToBasketAsync(int basketId, int productId, int quantity)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await GetCurrentUserAsync();
            Basket basket = await _basketReadRepository
                .GetSingleAsync(x => x.Id == basketId && x.CreatedByUserId == user.Id,
                                x => x.Include(b => b.BasketItems).ThenInclude(bi => bi.Product))
                ?? throw new NotFoundException("Basket Not Found");

            Product product = await _productReadRepository.GetByIdAsync(productId)
                ?? throw new NotFoundException("Product Not Found");
            BasketItem? basketItem = basket
                                .BasketItems
                                .FirstOrDefault(x => x.ProductId == productId);
            if (basketItem is not null)
            {
                basketItem.Quantity += quantity;
                basketItem.LineTotal = basketItem.Quantity * product.EstimatedPrice;
                UpdateQuantityDTO updateQuantityDTO = new()
                {
                    Quantity = basketItem.Quantity,
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
                    LineTotal = quantity * product.EstimatedPrice
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
        basketItem.LineTotal = dto.Quantity * basketItem.Product.EstimatedPrice;
        basket.TotalAmount += basketItem.LineTotal;
        await _basketWriteRepository.SaveAsync();
    }

    public async Task RemoveBasketAsync(int basketId)
    {
        var basket = await GetBasketByIdAsync(basketId);

        if (basket.BasketItems.Any())
            throw new InvalidOperationException("Cannot delete a basket that has items.");

        _basketWriteRepository.Remove(basket);
        await _basketWriteRepository.SaveAsync();
    }

    public async Task<Basket> DuplicateBasketAsync(int basketId, string newBasketName)
    {
        var sourceBasket = await GetBasketByIdAsync(basketId);
        var user = await GetCurrentUserAsync();

        var newBasket = new Basket
        {
            CreatedByUserId = user.Id,
            Name = newBasketName,
            // Description = sourceBasket.Description,
            CreatedDate = DateTime.UtcNow
        };

        await _basketWriteRepository.AddAsync(newBasket);
        await _basketWriteRepository.SaveAsync();

        foreach (var item in sourceBasket.BasketItems)
        {
            await AddItemToBasketAsync(
                newBasket.Id,
                item.ProductId,
                item.Quantity
            );
        }

        return newBasket;
    }

    public async Task SwitchPurchaseBasketItemAsync(int basketItemId)
    {
        BasketItem basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId)
            ?? throw new NotFoundException("Basket Item Not Found");

        basketItem.IsPurchased = basketItem.IsPurchased == true ? false : true;

        bool result = _basketItemWriteRepository.Update(basketItem);
        await _basketItemWriteRepository.SaveAsync();
    }

    public Task<BasketUser> CreateBasketUserAsync(int basketId, string userId)
    {
        // TODO: !!
        throw new NotImplementedException();
    }

    public async Task UpdateBasketAsync(int basketId, string newName)
    {
        var basket = await GetBasketByIdAsync(basketId);
        
        basket.Name = newName;
        
        _basketWriteRepository.Update(basket);
        await _basketWriteRepository.SaveAsync();
    }

    public async Task UpdatePlannedPurchaseDateAsync(int basketId, DateTime? plannedPurchaseDate)
    {
        var basket = await GetBasketByIdAsync(basketId);
        
        // basket.PlannedPurchaseDate = plannedPurchaseDate;
        
        _basketWriteRepository.Update(basket);
        await _basketWriteRepository.SaveAsync();
    }

    // Queries
    public async Task<List<Basket>> GetUserBasketsAsync()
    {
        AppUser? user = await GetCurrentUserAsync();

        var baskets = await _basketReadRepository
            .GetWhere(x => x.CreatedByUserId.Equals(user.Id))
            .Select(b => new Basket
            {
                Id = b.Id,
                CreatedByUserId = b.CreatedByUserId,
                Name = b.Name,
                // Description = b.Description,
                IsPurchased = b.IsPurchased,
                TotalAmount = b.BasketItems.Sum(bi => bi.LineTotal),
                // PlannedPurchaseDate = b.PlannedPurchaseDate,
                PurchasedDate = b.PurchasedDate,
                Note = b.Note
            })
            .ToListAsync();

        return baskets;
    }

    public async Task<Basket> GetBasketByIdAsync(int basketId)
    {
        var user = await GetCurrentUserAsync();
        
        var basket = await _basketReadRepository
            .GetWhere(x => x.Id == basketId && 
                          (x.CreatedByUserId == user.Id || 
                           x.BasketUsers.Any(bu => bu.AppUserId == user.Id)))
            .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
            .FirstOrDefaultAsync();

        if (basket is null)
            throw new NotFoundException("Basket not found or access denied");

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
        AppUser? user = await GetCurrentUserAsync();

        return await _basketReadRepository
            .GetWhere(x => x.CreatedByUserId.Equals(user.Id))
            .CountAsync();
    }

    public async Task<bool> IsBasketExistsAsync(int basketId)
    {
        return await _basketReadRepository.GetByIdAsync(basketId) != null;
    }

    public async Task<bool> HasUserAccessToBasket(int basketId)
    {
        var user = await GetCurrentUserAsync();
        var basket = await _basketReadRepository
            .GetWhere(x => x.Id == basketId)
            .Include(b => b.BasketUsers)
            .FirstOrDefaultAsync();

        if (basket == null) return false;

        return basket.CreatedByUserId == user.Id || 
               basket.BasketUsers.Any(bu => bu.AppUserId == user.Id);
    }

    // Private Helper Methods
    private async Task<AppUser> GetCurrentUserAsync()
    {
        string? userName = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(userName))
            throw new UnauthorizedException();

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        
        if (user == null)
            throw new UnauthorizedException();

        return user;
    }
}

using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.BasketUsers;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Domain.Entities;
using ShoppingList.Domain.Enums;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Services;

public class BasketUserService : IBasketUserService
{
    private readonly IBasketUserReadRepository _basketUserReadRepository;
    private readonly IBasketUserWriteRepository _basketUserWriteRepository;
    private readonly IBasketService _basketService;

    public BasketUserService(
        IBasketUserReadRepository basketUserReadRepository,
        IBasketUserWriteRepository basketUserWriteRepository,
        IBasketService basketService)
    {
        _basketUserReadRepository = basketUserReadRepository;
        _basketUserWriteRepository = basketUserWriteRepository;
        _basketService = basketService;
    }

    // Queries
    public async Task<List<BasketUser>> GetBasketUsersAsync(int basketId)
    {
        if (!await _basketService.HasUserAccessToBasket(basketId))
            throw new UnauthorizedException();

        return await _basketUserReadRepository
            .GetWhere(x => x.BasketId == basketId)
            .Include(bu => bu.AppUser)
            .ToListAsync();
    }

    public async Task<bool> IsUserInBasketAsync(int basketId, string userId)
    {
        return await _basketUserReadRepository
            .GetWhere(x => x.BasketId == basketId && x.AppUserId == userId)
            .AnyAsync();
    }

    public async Task<BasketUserRole> GetUserRoleInBasketAsync(int basketId, string userId)
    {
        var basketUser = await _basketUserReadRepository
            .GetWhere(x => x.BasketId == basketId && x.AppUserId == userId)
            .FirstOrDefaultAsync();

        if (basketUser == null)
            throw new NotFoundException("User is not in the basket");

        return basketUser.Role;
    }

    // Commands
    public async Task<BasketUser> AddUserToBasketAsync(int basketId, string userId, BasketUserRole role)
    {
        if (!await _basketService.HasUserAccessToBasket(basketId))
            throw new UnauthorizedException();

        if (await IsUserInBasketAsync(basketId, userId))
            throw new InvalidOperationException("User is already in the basket");

        var basketUser = new BasketUser
        {
            BasketId = basketId,
            AppUserId = userId,
            Role = role
        };

        await _basketUserWriteRepository.AddAsync(basketUser);
        await _basketUserWriteRepository.SaveAsync();

        return basketUser;
    }

    public async Task RemoveUserFromBasketAsync(int basketId, string userId)
    {
        if (!await _basketService.HasUserAccessToBasket(basketId))
            throw new UnauthorizedException();

        var basketUser = await _basketUserReadRepository
            .GetWhere(x => x.BasketId == basketId && x.AppUserId == userId)
            .FirstOrDefaultAsync();

        if (basketUser == null)
            throw new NotFoundException("User is not in the basket");

        if (basketUser.Role == BasketUserRole.Owner)
            throw new InvalidOperationException("Cannot remove the owner from the basket");

        _basketUserWriteRepository.Remove(basketUser);
        await _basketUserWriteRepository.SaveAsync();
    }

    public async Task UpdateUserRoleAsync(int basketId, string userId, BasketUserRole newRole)
    {
        if (!await _basketService.HasUserAccessToBasket(basketId))
            throw new UnauthorizedException();

        var basketUser = await _basketUserReadRepository
            .GetWhere(x => x.BasketId == basketId && x.AppUserId == userId)
            .FirstOrDefaultAsync();

        if (basketUser == null)
            throw new NotFoundException("User is not in the basket");

        if (basketUser.Role == BasketUserRole.Owner && newRole != BasketUserRole.Owner)
            throw new InvalidOperationException("Cannot change the owner's role");

        basketUser.Role = newRole;
        _basketUserWriteRepository.Update(basketUser);
        await _basketUserWriteRepository.SaveAsync();
    }
}
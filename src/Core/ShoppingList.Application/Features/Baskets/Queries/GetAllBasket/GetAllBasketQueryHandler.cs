using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Application.Features.Baskets.Queries.GetAllBasket;

public class GetAllBasketQueryHandler : IRequestHandler<GetAllBasketQueryRequest, IList<GetAllBasketQueryResponse>>
{
    private readonly IBasketService _basketService;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetAllBasketQueryHandler(IBasketService basketService, IBasketReadRepository basketReadRepository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _basketService = basketService;
        _basketReadRepository = basketReadRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IList<GetAllBasketQueryResponse>> Handle(GetAllBasketQueryRequest request, CancellationToken cancellationToken)
    {
        var baskets = await _basketService.GetBasketsAsync();

        List<GetAllBasketQueryResponse> response = new();
        foreach (var item in baskets)
        {
            response.Add(new GetAllBasketQueryResponse()
            {
                Id = item.Id,
                CreatedByUserId = item.CreatedByUserId,
                BasketName = item.BasketName,
                IsPurchased = item.IsPurchased,
                TotalAmount = item.TotalAmount,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                Items = item.BasketItems.Select(x => new BasketItemViewModel()
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    LineTotal = x.LineTotal
                }).ToList()
            });
        }
        return response;
    }
}
using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Baskets.Queries.GetByIdBasket;

public class GetByIdBasketQueryHandler : IRequestHandler<GetByIdBasketQueryRequest, GetByIdBasketQueryResponse>
{
    private readonly IBasketService _basketService;

    public GetByIdBasketQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<GetByIdBasketQueryResponse> Handle(GetByIdBasketQueryRequest request, CancellationToken cancellationToken)
    {
        Basket basket = await _basketService.GetBasketByIdAsync(request.BasketId);
        GetByIdBasketQueryResponse response = new()
        {
            Id = basket.Id,
            CreatedByUserId = basket.CreatedByUserId,
            Name = basket.Name,
            IsPurchased = basket.IsPurchased,
            TotalAmount = basket.TotalAmount,
            CreatedDate = basket.CreatedDate,
            UpdatedDate = basket.ModifiedDate,
            Items = basket.BasketItems.Select(x => new BasketItemDTO()
            {
                Id = x.Id,
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                LineTotal = x.LineTotal
            }).ToList()
        };
        return response;
    }
}
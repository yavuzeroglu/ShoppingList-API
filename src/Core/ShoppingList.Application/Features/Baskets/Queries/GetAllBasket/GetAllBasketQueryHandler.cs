using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;

namespace ShoppingList.Application.Features.Baskets.Queries.GetAllBasket;

public class GetAllBasketQueryHandler : IRequestHandler<GetAllBasketQueryRequest, IList<GetAllBasketQueryResponse>>
{
    private readonly IBasketService _basketService;
    private readonly IBasketItemService _basketItemService;

    public GetAllBasketQueryHandler(
        IBasketService basketService,
        IBasketItemService basketItemService)
    {
        _basketService = basketService;
        _basketItemService = basketItemService;
    }

    public async Task<IList<GetAllBasketQueryResponse>> Handle(GetAllBasketQueryRequest request, CancellationToken cancellationToken)
    {
        var baskets = await _basketService.GetUserBasketsAsync();
        var response = new List<GetAllBasketQueryResponse>();

        foreach (var basket in baskets)
        {
            var basketItems = await _basketItemService.GetBasketItemsAsync(basket.Id);

            response.Add(new GetAllBasketQueryResponse
            {
                Id = basket.Id,
                CreatedByUserId = basket.CreatedByUserId,
                BasketName = basket.Name,
                IsPurchased = basket.IsPurchased,
                TotalAmount = await _basketItemService.GetBasketTotalAsync(basket.Id),
                CreatedDate = basket.CreatedDate,
                UpdatedDate = basket.ModifiedDate,
                Items = basketItems.Select(x => new BasketItemDTO
                {
                    Id = x.Id,
                    AssignedUserId = x.AssignedUserId,
                    BasketId = x.ProductId,
                    AssignedUserName = x.AssignedUserName,
                    ProductId = x.ProductId,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    LineTotal = x.LineTotal,
                    IsPurchased = x.IsPurchased
                }).ToList()
            });
        }

        return response;
    }
}
using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;


namespace ShoppingList.Application.Features.Baskets.Queries.GetAllBasket;

public class GetAllBasketQueryHandler : IRequestHandler<GetAllBasketQueryRequest, IList<GetAllBasketQueryResponse>>
{
    private readonly IBasketService _basketService;
    public GetAllBasketQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;

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
                BasketName = item.Name,
                IsPurchased = item.IsPurchased,
                TotalAmount = item.TotalAmount,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.ModifiedDate,
                Items = item.BasketItems.Select(x => new BasketItemViewModel()
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    LineTotal = x.LineTotal,
                    IsPurchased = x.IsPurchased
                }).ToList()
            });
        }
        return response;
    }
}
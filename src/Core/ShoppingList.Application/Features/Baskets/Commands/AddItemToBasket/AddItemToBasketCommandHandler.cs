using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;

namespace ShoppingList.Application.Features.Baskets.Commands.ADdItemToBasket;

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
{
    private readonly IBasketService _basketService;

    public AddItemToBasketCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;

    }

    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.AddItemToBasketAsync(
            request.BasketId,
            request.ProductId,
            request.Quantity);

        return new AddItemToBasketCommandResponse()
        {
            IsSuccess = true,
            Message = "Ürün sepete başarıyla eklendi"
        };
    }
}
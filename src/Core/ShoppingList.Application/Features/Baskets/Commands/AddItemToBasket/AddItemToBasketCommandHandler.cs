using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;

namespace ShoppingList.Application.Features.Baskets.Commands.ADdItemToBasket;

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
{
    private readonly IBasketItemService _basketItemService;

    public AddItemToBasketCommandHandler(IBasketItemService basketItemService)
    {
        _basketItemService = basketItemService;
    }


    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketItemService.AddItemToBasketAsync(
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
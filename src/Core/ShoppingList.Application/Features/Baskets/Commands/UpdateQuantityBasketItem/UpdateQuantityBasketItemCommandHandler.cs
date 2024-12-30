using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Baskets.Commands.UpdateQuantityBasketItem;

public class UpdateQuantityBasketItemCommandHandler : IRequestHandler<UpdateQuantityBasketItemCommandRequest, UpdateQuantityBasketItemCommandResponse>
{
    private readonly IBasketService _basketService;

    public UpdateQuantityBasketItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<UpdateQuantityBasketItemCommandResponse> Handle(UpdateQuantityBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        var updateQuantityDTO = new UpdateQuantityDTO
        {
            Quantity = request.Quantity,
        };

        await _basketService.UpdateQuantityBasketItemAsync(request.BasketItemId, updateQuantityDTO);

        BasketItem basketItem = await _basketService.GetBasketItemByIdAsync(request.BasketItemId);
        Basket basket = await _basketService.GetBasketByIdAsync(basketItem.BasketId);

        return new UpdateQuantityBasketItemCommandResponse()
        {
            IsSuccess = true,
            Message = "Ürün miktarı başarıyla güncellendi",
            NewLineTotal = basketItem.LineTotal,
            NewBasketTotal = basket.TotalAmount
        };
    }

}
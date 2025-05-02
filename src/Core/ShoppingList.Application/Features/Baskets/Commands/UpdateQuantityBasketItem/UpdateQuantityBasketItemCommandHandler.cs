using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Baskets.Commands.UpdateQuantityBasketItem;

public class UpdateQuantityBasketItemCommandHandler : IRequestHandler<UpdateQuantityBasketItemCommandRequest, UpdateQuantityBasketItemCommandResponse>
{
    private readonly IBasketService _basketService;
    private readonly IBasketItemService _basketItemService;

    public UpdateQuantityBasketItemCommandHandler(IBasketService basketService, IBasketItemService basketItemService)
    {
        _basketService = basketService;
        _basketItemService = basketItemService;

    }

    public async Task<UpdateQuantityBasketItemCommandResponse> Handle(UpdateQuantityBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketItemService.UpdateQuantityAsync(request.BasketItemId, request.Quantity);

        BasketItem basketItem = await _basketItemService.GetBasketItemByIdAsync(request.BasketItemId);
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
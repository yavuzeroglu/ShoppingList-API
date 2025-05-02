using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;

namespace ShoppingList.Application.Features.Baskets.Commands.SwitchPurchaseBasketItem;

public class SwitchPurchaseBasketItemCommandHandler : IRequestHandler<SwitchPurchaseBasketItemCommandRequest, Unit>
{
    private readonly IBasketService _basketService;
    private readonly IBasketItemService _basketItemService;
    public SwitchPurchaseBasketItemCommandHandler(IBasketService basketService, IBasketItemService basketItemService)
    {
        _basketService = basketService;
        _basketItemService = basketItemService;

    }

    public async Task<Unit> Handle(SwitchPurchaseBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketItemService.TogglePurchaseStatusAsync(request.BasketItemId);
        return Unit.Value;
    }
}
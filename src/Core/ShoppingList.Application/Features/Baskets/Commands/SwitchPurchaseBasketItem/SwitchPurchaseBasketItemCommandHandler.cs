using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;

namespace ShoppingList.Application.Features.Baskets.Commands.SwitchPurchaseBasketItem;

public class SwitchPurchaseBasketItemCommandHandler : IRequestHandler<SwitchPurchaseBasketItemCommandRequest, Unit>
{
    private readonly IBasketService _basketService;
    public SwitchPurchaseBasketItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<Unit> Handle(SwitchPurchaseBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.SwitchPurchaseBasketItemAsync(request.BasketItemId);
        return Unit.Value;
    }
}
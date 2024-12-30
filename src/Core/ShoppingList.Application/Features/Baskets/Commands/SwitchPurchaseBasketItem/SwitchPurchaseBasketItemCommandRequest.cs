using MediatR;

namespace ShoppingList.Application.Features.Baskets.Commands.SwitchPurchaseBasketItem;

public class SwitchPurchaseBasketItemCommandRequest : IRequest<Unit>
{
    public int BasketItemId { get; set; }
}

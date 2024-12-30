using MediatR;

namespace ShoppingList.Application.Features.Baskets.Commands.UpdateQuantityBasketItem;

public sealed record UpdateQuantityBasketItemCommandRequest : IRequest<UpdateQuantityBasketItemCommandResponse>
{
    public int BasketItemId { get; init; }
    public int Quantity { get; init; }
}

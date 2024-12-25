using MediatR;

namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasketItem;

public sealed record RemoveBasketItemCommandRequest : IRequest<RemoveBasketItemCommandResponse>
{
    public int BasketItemId { get; init; }
}

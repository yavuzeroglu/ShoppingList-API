using MediatR;

namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasket;


public sealed class RemoveBasketCommandRequest : IRequest<RemoveBasketCommandResponse>
{
    public int BasketId { get; set; }
}


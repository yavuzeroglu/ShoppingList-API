using MediatR;
using ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;

namespace ShoppingList.Application.Features.Baskets.Commands.CreateBasket;

public sealed record CreateBasketCommandRequest : IRequest<CreateBasketCommandResponse>
{
   public string Name { get; init; } = string.Empty;
}

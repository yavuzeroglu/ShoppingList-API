using MediatR;
using ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;

namespace ShoppingList.Application.Features.Baskets.Commands.ADdItemToBasket;

public class AddItemToBasketCommandRequest : IRequest<AddItemToBasketCommandResponse>
{
  public int BasketId { get; set; }
  public int ProductId { get; set; }
  public int Quantity { get; set; }
}

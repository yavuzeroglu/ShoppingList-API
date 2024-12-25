using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Domain.Entities;

public class BasketItem : BaseEntity
{
  public int BasketId { get; set; }
  public int ProductId { get; set; }

  public int Quantity { get; set; }
  public decimal LineTotal { get; set; }

  public Basket Basket { get; set; }
  public Product Product { get; set; }
}
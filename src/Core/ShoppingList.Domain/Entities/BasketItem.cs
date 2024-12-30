using ShoppingList.Domain.Entities.Common;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Domain.Entities;

public class BasketItem : BaseEntity
{
  public int BasketId { get; set; }
  public int ProductId { get; set; }
  private int _quantity;
  public int Quantity
  {
    get => _quantity;
    set => _quantity = value > 0 ? value : 1;
  }
  public decimal LineTotal { get; set; }
  public string? AssignedUserId { get; set; }
  public bool IsPurchased { get; set; } = false;

  // Navigation Properties
  public Basket? Basket { get; set; }
  public Product? Product { get; set; }
  public AppUser? AssignedUser { get; set; }
}

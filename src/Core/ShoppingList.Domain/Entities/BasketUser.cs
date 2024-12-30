using ShoppingList.Domain.Entities.Common;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Domain.Entities;

public class BasketUser : BaseEntity
{
  public BasketUser()
  {
    Basket = new();
    AppUser = new();
  }

  public int BasketId { get; set; }
  public string AppUserId { get; set; } = string.Empty;
  
  // Navigation Properties
  public Basket Basket { get; set; }
  public AppUser AppUser { get; set; }
}
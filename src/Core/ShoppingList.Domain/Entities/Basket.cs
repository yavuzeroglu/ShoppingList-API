using ShoppingList.Domain.Entities.Common;
using ShoppingList.Domain.Entities.Identity;

namespace ShoppingList.Domain.Entities;


public class Basket : BaseEntity
{
  public Basket()
  {
    BasketItems = new List<BasketItem>();
    BasketUsers = new List<BasketUser>();
  }

  public string CreatedByUserId { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public bool IsPurchased { get; set; } = false;
  public decimal TotalAmount { get; set; }
  public DateTime PurchasedDate { get; set; }
  public string Note { get; set; } = string.Empty;

  // Navigation Properties
  public AppUser? CreatedByUser { get; set; }
  public ICollection<BasketItem> BasketItems { get; set; }
  public ICollection<BasketUser> BasketUsers { get; set; }
}

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
  public string BasketName { get; set; } = string.Empty;
  public bool IsPurchased { get; set; } = false;
  public decimal TotalAmount { get; set; }
  public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedDate { get; set; }
  public AppUser CreatedByUser { get; set; }
  public ICollection<BasketItem> BasketItems { get; set; }
  public ICollection<BasketUser> BasketUsers { get; set; }
}

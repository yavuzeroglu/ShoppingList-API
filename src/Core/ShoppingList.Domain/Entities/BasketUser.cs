using ShoppingList.Domain.Entities.Common;
using ShoppingList.Domain.Entities.Identity;
using ShoppingList.Domain.Enums;


namespace ShoppingList.Domain.Entities;

public class BasketUser : BaseEntity
{
  public int BasketId { get; set; }
  public string AppUserId { get; set; } = string.Empty;
  public BasketUserRole Role { get; set; } = BasketUserRole.Viewer;
  
  // Navigation Properties
  public Basket Basket { get; set; }
  public AppUser AppUser { get; set; }
}
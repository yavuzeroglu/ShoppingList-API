using ShoppingList.Domain.Entities.Common;
using ShoppingList.Domain.Enums;

namespace ShoppingList.Domain.Entities;

public class Product : BaseEntity
{
   public Product()
   {
      Images = new HashSet<Image>();
   }
   public int CategoryId { get; set; }
   public int BrandId { get; set; }
   public string Name { get; set; } = string.Empty;
   public bool IsActive { get; set; }
   public decimal EstimatedPrice { get; set; }
   public UnitType Unit { get; set; }

   // Navigation Properties
   public Category? Category { get; set; }
   public Brand? Brand { get; set; }
   public ICollection<Image>? Images { get; set; }
}

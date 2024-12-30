using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Domain.Entities;

public class Brand : BaseEntity
{
   public string Name { get; set; } = string.Empty;
   

   // Navigation Properties
   public ICollection<Product>? Products { get; set; }
}
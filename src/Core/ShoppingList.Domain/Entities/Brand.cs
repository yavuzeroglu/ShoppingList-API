using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Domain.Entities;

public class Brand : BaseEntity
{
   public string Name { get; set; }
   public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
   
   public ICollection<Product> Products { get; set; }
}
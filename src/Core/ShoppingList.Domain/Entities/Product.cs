using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Domain.Entities;

public class Product : BaseEntity
{
   public Product()
   {
      Images = new HashSet<Image>();
   }
   public int CategoryId { get; set; }
   public int BrandId { get; set; }
   public string Name { get; set; }
   public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
   public bool IsActive { get; set; }


   public Category Category { get; set; }
   public Brand Brand { get; set; }
   public ICollection<Image> Images { get; set; }
}

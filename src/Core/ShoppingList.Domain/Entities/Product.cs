namespace ShoppingList.Domain.Entities;

public class Product
{
   public int CategoryId { get; set; }
   public string Name { get; set; }
   public DateTime CreatedDate { get; set; }
   public bool IsActive { get; set; }
   public Category Category { get; set; }
}

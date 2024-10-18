using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Domain.Entities;

public class Image : BaseEntity
{
   public int ProductId { get; set; }
   public string FileName { get; set; }
   public string Path { get; set; }
   
   public Product Product { get; set; }
}
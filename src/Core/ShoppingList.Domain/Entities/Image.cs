using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Domain.Entities;

public class Image : BaseEntity
{
   public int ProductId { get; set; }
   public string FileName { get; set; } = string.Empty;
   public string Path { get; set; } = string.Empty;
   
   public Product Product { get; set; }
}
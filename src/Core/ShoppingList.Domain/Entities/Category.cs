using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Domain.Entities;

public class Category : BaseEntity
{
   public string Name { get; set; } = string.Empty;
   public int? ParentCategoryId { get; set; }


   // Navigation Properties
   public Category? ParentCategory { get; set; }
   public ICollection<Category>? SubCategories { get; set; }
   public ICollection<Product>? Products { get; set; }
}
namespace ShoppingList.Application.DTOs.Categories;

public class ListCategoryDTO
{
   public string Name { get; set; } = string.Empty;
   public string? ParentCategoryName { get; set; }
   public List<string> SubCategories { get; set; } = new();
   public List<string> Products { get; set; } = new();
}
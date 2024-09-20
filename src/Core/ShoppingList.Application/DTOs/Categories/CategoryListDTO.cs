namespace ShoppingList.Application.DTOs.Categories;

public class ListCategoryDTO
{
   public string Name { get; set; }
   public string? ParentCategoryName { get; set; }
   public List<string> SubCategories { get; set; }
   public List<string> Products { get; set; }
}
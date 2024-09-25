namespace ShoppingList.Application.Features.Categories.Queries.GetByIdCategory;

public class GetByIdCategoryQueryResponse
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public int? ParentCategoryId { get; set; }
   public List<string>? SubCategories { get; set; }
}
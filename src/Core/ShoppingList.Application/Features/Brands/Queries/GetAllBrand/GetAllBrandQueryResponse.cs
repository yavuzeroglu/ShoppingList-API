namespace ShoppingList.Application.Features.Brands.Queries.GetAllBrand;

public class GetAllBrandQueryResponse
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public List<string>? Products { get; set; }
}

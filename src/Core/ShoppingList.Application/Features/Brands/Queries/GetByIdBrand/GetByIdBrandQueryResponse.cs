namespace ShoppingList.Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQueryResponse
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public List<string>? ProductNames { get; set; }
}

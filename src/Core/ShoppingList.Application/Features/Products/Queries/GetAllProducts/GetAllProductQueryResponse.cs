namespace ShoppingList.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductQueryResponse
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public string CategoryName { get; set; } = string.Empty;
   public string BrandName { get; set; } = string.Empty;
   public bool IsActive { get; set; }
   public string CreatedDate { get; set; }
}
using ShoppingList.Domain.Enums;

namespace ShoppingList.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductQueryResponse
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public int CategoryId { get; set; }
   public string Unit { get; set; } = string.Empty;
   public decimal EstimatedPrice {get; set;}
   public int BrandId { get; set; }
   public List<string>? Images { get; set; }
   public bool IsActive { get; set; }
   public string CreatedDate { get; set; } = string.Empty;
}
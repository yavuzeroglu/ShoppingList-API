namespace ShoppingList.Application.Features.Products.Queries.GetByIdProduct;


public class GetByIdProductQueryResponse
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public int CategoryId { get; set; }
   public int BrandId { get; set; }
   public bool IsActive { get; set; }
   public string CreatedDate { get; set; } = string.Empty;
}
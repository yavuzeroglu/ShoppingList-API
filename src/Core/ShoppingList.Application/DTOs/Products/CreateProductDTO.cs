namespace ShoppingList.Application.DTOs.Products;

public record CreateProductDTO
{
   public int CategoryId { get; init; }
   public int BrandId { get; init; }
   public string Name { get; init; } = string.Empty;
   public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
   public bool IsActive { get; init; } = true;
}

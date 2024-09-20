namespace ShoppingList.Application.DTOs.Products;

public class UpdateProductDTO
{
   public int Id { get; init; }
   public int CategoryId { get; init; }
   public int BrandId { get; init; }
   public string Name { get; init; } = string.Empty;
   public bool IsActive { get; init; } = true;
}
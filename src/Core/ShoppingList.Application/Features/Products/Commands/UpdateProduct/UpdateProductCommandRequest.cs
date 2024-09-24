using MediatR;

namespace ShoppingList.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandRequest : IRequest
{
   public int Id { get; set; }
   public int CategoryId { get; init; }
   public int BrandId { get; init; }
   public string Name { get; init; } = string.Empty;
   // public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
   public bool IsActive { get; init; } = true;
}
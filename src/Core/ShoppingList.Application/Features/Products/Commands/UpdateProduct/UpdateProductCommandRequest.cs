using MediatR;

namespace ShoppingList.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandRequest : IRequest<Unit>
{
   public int Id { get; set; }
   public int CategoryId { get; init; }
   public int BrandId { get; init; }
   public string Name { get; init; } = string.Empty;
   public bool IsActive { get; init; } = true;
}
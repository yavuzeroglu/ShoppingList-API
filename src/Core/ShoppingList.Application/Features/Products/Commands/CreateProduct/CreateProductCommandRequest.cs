using MediatR;
using Microsoft.AspNetCore.Http;

namespace ShoppingList.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandRequest : IRequest<Unit>
{
   public int CategoryId { get; init; }
   public int BrandId { get; init; }
   public string Name { get; init; } = string.Empty;
   public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
   public bool IsActive { get; init; } = true;
   public IFormFile? Photo { get; set; }
}
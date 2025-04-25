using MediatR;
using ShoppingList.Application.DTOs.Products;

namespace ShoppingList.Application.Features.Products.Commands.PatchProduct;

public class PatchProductCommandRequest : IRequest<Unit>
{
     public int Id { get; set; }  // Id'yi geri ekliyoruz
    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public decimal? EstimatedPrice { get; set; }
    public string? Unit { get; set; }  
}
using MediatR;

namespace ShoppingList.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandRequest : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
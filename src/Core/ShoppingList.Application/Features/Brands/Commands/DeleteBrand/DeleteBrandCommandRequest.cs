using MediatR;

namespace ShoppingList.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandRequest : IRequest<Unit>
{
   public int Id { get; set; }
}

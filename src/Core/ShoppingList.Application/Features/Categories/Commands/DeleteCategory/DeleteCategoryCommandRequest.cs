using MediatR;

namespace ShoppingList.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandRequest : IRequest<Unit>
{
   public int Id { get; set; }
}
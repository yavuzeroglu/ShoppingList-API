using MediatR;

namespace ShoppingList.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandRequest : IRequest<Unit>
{
   public string Name { get; set; } = string.Empty;
   public int? ParentCategoryId { get; set; }
}

using MediatR;

namespace ShoppingList.Application.Features.Categories.Queries.GetByIdCategory;

public class GetByIdCategoryQueryRequest : IRequest<GetByIdCategoryQueryResponse>
{
   public int Id { get; set; }
}

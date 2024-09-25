using MediatR;

namespace ShoppingList.Application.Features.Categories.Queries.GetAllCategory;

public class GetAllCategoryQueryRequest : IRequest<IList<GetAllCategoryQueryResponse>>
{

}

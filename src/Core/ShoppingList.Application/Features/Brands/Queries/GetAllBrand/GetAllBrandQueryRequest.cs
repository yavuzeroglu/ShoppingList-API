using MediatR;

namespace ShoppingList.Application.Features.Brands.Queries.GetAllBrand;

public class GetAllBrandQueryRequest : IRequest<IList<GetAllBrandQueryResponse>> 
{

}

using MediatR;

namespace ShoppingList.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductQueryRequest : IRequest<IList<GetAllProductQueryResponse>>
{

}
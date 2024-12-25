using MediatR;

namespace ShoppingList.Application.Features.Baskets.Queries.GetAllBasket;

public class GetAllBasketQueryRequest : IRequest<IList<GetAllBasketQueryResponse>>
{
}

using MediatR;

namespace ShoppingList.Application.Features.Baskets.Queries.GetByIdBasket;

public class GetByIdBasketQueryRequest : IRequest<GetByIdBasketQueryResponse>
{
    public int BasketId { get; set; }
}

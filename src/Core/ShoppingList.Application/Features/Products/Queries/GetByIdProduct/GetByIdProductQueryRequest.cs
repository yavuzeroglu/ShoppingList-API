using MediatR;

namespace ShoppingList.Application.Features.Products.Queries.GetByIdProduct;

public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
{
   public int Id { get; set; }
}
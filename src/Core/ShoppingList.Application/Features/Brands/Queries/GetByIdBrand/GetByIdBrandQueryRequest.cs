using MediatR;

namespace ShoppingList.Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQueryRequest : IRequest<GetByIdBrandQueryResponse>
{
    public int Id { get; set; }
}

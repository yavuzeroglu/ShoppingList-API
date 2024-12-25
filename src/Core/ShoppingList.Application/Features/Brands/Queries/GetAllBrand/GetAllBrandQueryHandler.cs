using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Common.Abstractions.Repositories.Brands;

namespace ShoppingList.Application.Features.Brands.Queries.GetAllBrand;

public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQueryRequest, IList<GetAllBrandQueryResponse>>
{
    private readonly IBrandReadRepository _brandReadRepository;

    public GetAllBrandQueryHandler(IBrandReadRepository brandReadRepository)
    {
        _brandReadRepository = brandReadRepository;
    }

    public Task<IList<GetAllBrandQueryResponse>> Handle(GetAllBrandQueryRequest request, CancellationToken cancellationToken)
    {
        var brands = _brandReadRepository
         .GetAll()
         .Include(b => b.Products)
         .OrderBy(b => b.Id);

        List<GetAllBrandQueryResponse> response = new();

        foreach (var br in brands)
            response.Add(new GetAllBrandQueryResponse()
            {
                Id = br.Id,
                Name = br.Name,
                Products = br.Products?.Select(p => p.Name).ToList()
            });

        return Task.FromResult<IList<GetAllBrandQueryResponse>>(response);
    }
}
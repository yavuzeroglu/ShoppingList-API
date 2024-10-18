using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Abstractions.Repositories.ProductImage;
using ShoppingList.Application.Abstractions.Repositories.Products;

namespace ShoppingList.Application.Features.Products.Queries.GetAllProducts;
public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
{
   private readonly IProductImageReadRepository _productImageReadRepository;
   private readonly IProductReadRepository _productReadRepository;
    public GetAllProductQueryHandler(IProductReadRepository productReadRepository, IProductImageReadRepository productImageReadRepository)
    {
        _productReadRepository = productReadRepository;
        _productImageReadRepository = productImageReadRepository;
    }

    public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
   {
      var products = _productReadRepository
         .GetAll()
         .OrderBy(p => p.Id);
      List<GetAllProductQueryResponse> response = new();
      var images = await _productImageReadRepository.Table.ToListAsync();

      foreach (var product in products)
         response.Add(new GetAllProductQueryResponse()
         {
            Id = product.Id,
            Name = product.Name,
            BrandId = product.BrandId,
            CategoryId = product.CategoryId,
            IsActive = product.IsActive,
            Images = images.Where(i => i.ProductId.Equals(product.Id)).Select(i => i.FileName).ToList(),
            CreatedDate = product.CreatedDate.ToString("dd-MM-yyyy")
         });

      return response;
   }
}
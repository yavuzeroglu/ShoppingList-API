using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Repositories.Products;

namespace ShoppingList.Application.Features.Products.Queries.GetAllProducts;
public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
{
   private readonly IProductReadRepository _productReadRepository;

   public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
   {
      _productReadRepository = productReadRepository;
   }

   public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
   {
      var products = _productReadRepository
         .GetAll()
         .Include(p => p.Category)
         .Include(p => p.Brand)
         .OrderBy(p => p.Id);
      List<GetAllProductQueryResponse> response = new();

      foreach (var product in products)
         response.Add(new GetAllProductQueryResponse()
         {
            Id = product.Id,
            Name = product.Name,
            BrandName = product.Brand.Name,
            CategoryName = product.Category.Name,
            IsActive = product.IsActive,
            CreatedDate = product.CreatedDate.ToString("dd-mm-yyyy")
         });

      return response;
   }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Repositories.Products;

namespace ShoppingList.Application.Features.Products.Queries.GetByIdProduct;


public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
   private readonly IProductReadRepository _productReadRepository;

   public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
   {
      _productReadRepository = productReadRepository;
   }

   public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
   {
      var product = await _productReadRepository
       .GetWhere(p => p.Id == request.Id)
       .Include(p => p.Brand)
       .Include(p => p.Category)
       .FirstOrDefaultAsync();


      if (product is null)
         throw new NotFoundException("Product Not Found");

      var response = new GetByIdProductQueryResponse()
      {
         Id = product.Id,
         Name = product.Name,
         BrandId = product.BrandId,
         CategoryId = product.CategoryId,
         IsActive = product.IsActive,
         CreatedDate = product.CreatedDate.ToString("dd-MM-yyyy")
      };
      return response;
   }
}
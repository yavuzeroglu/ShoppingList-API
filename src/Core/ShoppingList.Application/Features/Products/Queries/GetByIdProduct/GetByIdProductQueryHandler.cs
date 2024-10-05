using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Repositories.Products;

namespace ShoppingList.Application.Features.Products.Queries.GetByIdProduct;


public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
   private readonly IProductReadRepository _productReadRepository;
   private readonly ILogger<GetByIdProductQueryHandler> _logger;
    public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetByIdProductQueryHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _logger = logger;
    }

    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
   {
      var product = await _productReadRepository
       .GetWhere(p => p.Id == request.Id)
       .Include(p => p.Brand)
       .Include(p => p.Category)
       .FirstOrDefaultAsync();


      if (product is null)
      {
         _logger.LogWarning("Product Not Found");
         throw new NotFoundException("Product Not Found");
      }

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
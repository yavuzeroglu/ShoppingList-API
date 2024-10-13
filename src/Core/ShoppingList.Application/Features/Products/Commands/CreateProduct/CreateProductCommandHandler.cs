using MediatR;
using ShoppingList.Application.Abstractions.Repositories.Products;
using ShoppingList.Domain.Entities;
using ShoppingList.Application.Abstractions.Storage;
using ShoppingList.Application.Abstractions.Repositories.ProductImage;

namespace ShoppingList.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
   private readonly IProductWriteRepository _productWriteRepository;
   public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
   {
      _productWriteRepository = productWriteRepository;
      
   }

   public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
   {
      Product product = new()
      {
         Name = request.Name,
         BrandId = request.BrandId,
         CategoryId = request.CategoryId,
         IsActive = request.IsActive,
         CreatedDate = request.CreatedDate
      };
      await _productWriteRepository.AddAsync(product);
      await _productWriteRepository.SaveAsync();
      return Unit.Value;
   }
}
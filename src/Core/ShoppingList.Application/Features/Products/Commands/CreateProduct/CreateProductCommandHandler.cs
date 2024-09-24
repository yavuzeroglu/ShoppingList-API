using MediatR;
using ShoppingList.Application.Repositories.Products;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
{
   private readonly IProductWriteRepository _productWriteRepository;

   public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
   {
      _productWriteRepository = productWriteRepository;
   }

   public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
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
   }
}
using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;

namespace ShoppingList.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, Unit>
{

   private readonly IProductReadRepository _productReadRepository;
   private readonly IProductWriteRepository _productWriteRepository;

   public DeleteProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
   {
      _productReadRepository = productReadRepository;
      _productWriteRepository = productWriteRepository;
   }

   public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
   {
      var product = await _productReadRepository.GetSingleAsync(p => p.Id == request.Id && p.IsActive, tracking: false);
      if (product is null)
         throw new NotFoundException("Product Not Found");

      product.IsActive = false;
      _productWriteRepository.Update(product);
      await _productWriteRepository.SaveAsync();

      return Unit.Value;
   }
}
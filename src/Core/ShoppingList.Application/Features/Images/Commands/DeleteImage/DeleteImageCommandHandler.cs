using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Common.Abstractions.Repositories.ProductImage;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
using ShoppingList.Application.Common.Abstractions.Storage;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Images.Commands.DeleteImage;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommandRequest, Unit>
{
  private readonly IProductReadRepository _productReadRepository;
  private readonly IProductImageWriteRepository _productImageWriteRepository;
  private readonly IProductImageReadRepository _productImageReadRepository;
  private readonly IStorage _storageService;
  public DeleteImageCommandHandler(IProductReadRepository productReadRepository, IProductImageWriteRepository productImageWriteRepository, IStorageService storageService, IProductImageReadRepository productImageReadRepository)
  {
    _productReadRepository = productReadRepository;
    _productImageWriteRepository = productImageWriteRepository;
    _storageService = storageService;
    _productImageReadRepository = productImageReadRepository;
  }

  public async Task<Unit> Handle(DeleteImageCommandRequest request, CancellationToken cancellationToken)
  {
    Product? product = await _productReadRepository.GetSingleAsync(p => p.Id.Equals(request.ProductId), include: i => i.Include(s => s.Images));
    Image? productImage = await _productImageReadRepository.GetByIdAsync(request.ImageId, false);
    
    await _storageService.DeleteAsync(pathOrContainer: productImage.Path, fileName: productImage.FileName);
    product.Images.Remove(productImage);
    
    await _productImageWriteRepository.SaveAsync();
    return Unit.Value;
  }
}


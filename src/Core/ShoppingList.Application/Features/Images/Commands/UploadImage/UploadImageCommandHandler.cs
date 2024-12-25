using MediatR;
using ShoppingList.Application.Common.Abstractions.Repositories.ProductImage;
using ShoppingList.Application.Common.Abstractions.Storage;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Images.Commands.UploadImage;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommandRequest, Unit>
{
  private readonly IStorageService _storageService;
  private readonly IProductImageWriteRepository _productImageWriteRepository;

  public UploadImageCommandHandler(IStorageService storageService, IProductImageWriteRepository productImageWriteRepository)
  {
    _storageService = storageService;
    _productImageWriteRepository = productImageWriteRepository;
  }

  public async Task<Unit> Handle(UploadImageCommandRequest request, CancellationToken cancellationToken)
  {
    // TODO: check product

    (string path, string fileName) = await _storageService.UploadAsync("images", request.Photo);
    var image = new Image()
    {
      FileName = fileName,
      Path = path,
      ProductId = request.ProductId,
    };
    await _productImageWriteRepository.AddAsync(image);
    await _productImageWriteRepository.SaveAsync();

    return Unit.Value;
  }
}

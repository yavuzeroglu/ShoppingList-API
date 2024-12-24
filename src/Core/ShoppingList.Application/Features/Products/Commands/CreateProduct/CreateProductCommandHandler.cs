using MediatR;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
using ShoppingList.Domain.Entities;
using ShoppingList.Application.Common.Abstractions.Storage;
using AutoMapper;

namespace ShoppingList.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
   private readonly IStorageService _storageService;
   private readonly IProductWriteRepository _productWriteRepository;
   private readonly IMapper _mapper;
   public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IStorageService storageService, IMapper mapper)
   {
      _productWriteRepository = productWriteRepository;
      _storageService = storageService;
      _mapper = mapper;
   }

   public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
   {
      Product product = _mapper.Map<Product>(request);
      if (request.Photo is not null)
      {
         (string pathOrContainer, string fileName) uploadImage = await _storageService.UploadAsync("images", request.Photo);

         product.Images.Add(new Image()
         {
            Path = uploadImage.pathOrContainer,
            FileName = uploadImage.fileName,
            ProductId = product.Id
         });
      }

      await _productWriteRepository.AddAsync(product);
      await _productWriteRepository.SaveAsync();
      return Unit.Value;
   }
}
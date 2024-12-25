using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, Unit>
{
   private readonly IProductReadRepository _productReadRepository;
   private readonly IProductWriteRepository _productWriteRepository;
   private readonly IMapper _mapper;

   public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper)
   {
      _productReadRepository = productReadRepository;
      _productWriteRepository = productWriteRepository;
      _mapper = mapper;
   }

   public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
   {
      var product = await _productReadRepository
         .GetSingleAsync(p => p.Id == request.Id && p.IsActive, tracking: false);
      if (product is null)
         throw new NotFoundException("Product Not Found");

      var mappedEntity = _mapper.Map<UpdateProductCommandRequest, Product>(request);

      _productWriteRepository.Update(mappedEntity);
      await _productWriteRepository.SaveAsync();

      return Unit.Value;
   }
}
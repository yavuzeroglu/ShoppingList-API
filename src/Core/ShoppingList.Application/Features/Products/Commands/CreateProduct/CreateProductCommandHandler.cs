using FluentValidation;
using MediatR;
using ShoppingList.Application.Exceptions;
using ShoppingList.Application.Repositories.Products;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
   private readonly IProductWriteRepository _productWriteRepository;
   private readonly CreateProductCommandValidator _validator;

   public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, CreateProductCommandValidator validator)
   {
      _productWriteRepository = productWriteRepository;
      _validator = validator;
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
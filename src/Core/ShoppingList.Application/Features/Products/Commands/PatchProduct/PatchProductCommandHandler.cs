using System.ComponentModel.DataAnnotations;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
using ShoppingList.Application.Features.Products.Commands.PatchProduct;
using ShoppingList.Domain.Entities;
using ShoppingList.Domain.Enums;

namespace ShoppingList.Application.Features.Products.Commands.request;


public class PatchProductCommandHandler : IRequestHandler<PatchProductCommandRequest, Unit>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public PatchProductCommandHandler(
        IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<Unit> Handle(PatchProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository
            .GetSingleAsync(p => p.Id == request.Id && p.IsActive, tracking: false);

        if (product is null)
            throw new NotFoundException("Ürün Bulunamadı");

        

        if (request.Name is not null)
            product.Name = request.Name;

        if (request.CategoryId.HasValue)
            product.CategoryId = request.CategoryId.Value;

        if (request.BrandId.HasValue)
            product.BrandId = request.BrandId.Value;
            
        if (request.IsActive.HasValue)
            product.IsActive = request.IsActive.Value;
            
        if (request.EstimatedPrice > 0)
            product.EstimatedPrice = (decimal)request.EstimatedPrice;
            
        if (!string.IsNullOrEmpty(request.Unit))
        {
            if (Enum.TryParse<UnitType>(request.Unit, true, out UnitType unitType))
            {
                product.Unit = unitType;
            }
            else
            {
                throw new ValidationException(
                    $"Geçersiz birim tipi. Geçerli değerler: {string.Join(", ", Enum.GetNames<UnitType>())}");
            }
        }

        _productWriteRepository.Update(product);
        await _productWriteRepository.SaveAsync();

        return Unit.Value;
    }
}
using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Brands;
namespace ShoppingList.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommandRequest, Unit>
{
    private readonly IBrandReadRepository _brandReadRepository;
    private readonly IBrandWriteRepository _brandWriteRepository;

    public UpdateBrandCommandHandler(IBrandReadRepository brandReadRepository, IBrandWriteRepository brandWriteRepository)
    {
        _brandReadRepository = brandReadRepository;
        _brandWriteRepository = brandWriteRepository;
    }

    public async Task<Unit> Handle(UpdateBrandCommandRequest request, CancellationToken cancellationToken)
    {
        var brand = await _brandReadRepository.GetByIdAsync(request.Id, false);
        if (brand is null)
            throw new NotFoundException("Brand Not Found");

        brand.Name = request.Name;
        _brandWriteRepository.Update(brand);
        await _brandWriteRepository.SaveAsync();

        return Unit.Value;
    }
}

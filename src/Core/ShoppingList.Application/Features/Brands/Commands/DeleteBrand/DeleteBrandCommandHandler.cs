using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Repositories.Brands;

namespace ShoppingList.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommandRequest, Unit>
{
   private readonly IBrandReadRepository _brandReadRepository;
   private readonly IBrandWriteRepository _brandWriteRepository;

   public DeleteBrandCommandHandler(IBrandReadRepository brandReadRepository, IBrandWriteRepository brandWriteRepository)
   {
      _brandReadRepository = brandReadRepository;
      _brandWriteRepository = brandWriteRepository;
   }

   public async Task<Unit> Handle(DeleteBrandCommandRequest request, CancellationToken cancellationToken)
   {
      var brand =  await _brandReadRepository.GetByIdAsync(request.Id, false);
      if (brand is null)
         throw new NotFoundException("Brand Not Found");

      _brandWriteRepository.Remove(brand);
      await _brandWriteRepository.SaveAsync();

      return Unit.Value;
   }
}
using MediatR;
using ShoppingList.Application.Common.Abstractions.Repositories.Brands;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommandRequest, Unit>
{
   private readonly IBrandReadRepository _brandReadRepository;
   private readonly IBrandWriteRepository _brandWriteRepository;

   public CreateBrandCommandHandler(IBrandReadRepository brandReadRepository, IBrandWriteRepository brandWriteRepository)
   {
      _brandReadRepository = brandReadRepository;
      _brandWriteRepository = brandWriteRepository;
   }

   public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
   {
      var brand = await _brandReadRepository
         .GetSingleAsync(p => p.Name.ToLower().Equals(request.Name.ToLower()));
      if (brand is not null)
         throw new InvalidOperationException("Aynı isimde marka mevcut");

      Brand newBrand = new()
      {
         Name = request.Name,
         CreatedDate = DateTime.UtcNow
      };

      await _brandWriteRepository.AddAsync(newBrand);
      await _brandWriteRepository.SaveAsync();
      return Unit.Value;
   }
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Brands;

namespace ShoppingList.Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQueryRequest, GetByIdBrandQueryResponse>
{
   private readonly IBrandReadRepository _brandReadRepository;
   private readonly IMapper _mapper;

   public GetByIdBrandQueryHandler(IBrandReadRepository brandReadRepository, IMapper mapper)
   {
      _brandReadRepository = brandReadRepository;
      _mapper = mapper;
   }

   public async Task<GetByIdBrandQueryResponse> Handle(GetByIdBrandQueryRequest request, CancellationToken cancellationToken)
   {
      var brand = await _brandReadRepository
         .GetSingleAsync(b => b.Id == request.Id, include: x => x.Include(b => b.Products));
      
      if (brand is null)
         throw new NotFoundException("Brand not found");

      GetByIdBrandQueryResponse response = new()
      {
         Id = brand.Id,
         Name = brand.Name,
         ProductNames = brand.Products.Select(p => p.Name).ToList()
      };
      
      return response;
   }
}
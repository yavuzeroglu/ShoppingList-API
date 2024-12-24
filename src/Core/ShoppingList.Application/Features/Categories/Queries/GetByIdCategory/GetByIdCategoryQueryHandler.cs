using AutoMapper;
using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Categories;

namespace ShoppingList.Application.Features.Categories.Queries.GetByIdCategory;

public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQueryRequest, GetByIdCategoryQueryResponse>
{
   private readonly ICategoryReadRepository _categoryReadRepository;
   private readonly IMapper _mapper;
    public GetByIdCategoryQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper)
    {
        _categoryReadRepository = categoryReadRepository;
        _mapper = mapper;
    }

    public async Task<GetByIdCategoryQueryResponse> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
   {
      var category = await _categoryReadRepository.GetByIdAsync(request.Id, false);

      if(category is null)
         throw new NotFoundException("Category Not Found");

      var mappedResponse = _mapper.Map<GetByIdCategoryQueryResponse>(category);
      return mappedResponse;
   }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Common.Abstractions.Repositories.Categories;

namespace ShoppingList.Application.Features.Categories.Queries.GetAllCategory;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, IList<GetAllCategoryQueryResponse>>
{
   private readonly ICategoryReadRepository _categoryReadRepository;

   public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository)
   {
      _categoryReadRepository = categoryReadRepository;
   }

   public Task<IList<GetAllCategoryQueryResponse>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
   {
      var categories = _categoryReadRepository
         .GetAll()
         .Include(c => c.ParentCategory)
         .Include(c => c.SubCategories)
         .OrderBy(c => c.Id);
      List<GetAllCategoryQueryResponse> response = new();

      foreach (var category in categories)
         response.Add(new GetAllCategoryQueryResponse()
         {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId ?? null,
            SubCategories = category.SubCategories?.Select(sc => sc.Name).ToList()
         });

      return Task.FromResult<IList<GetAllCategoryQueryResponse>>(response);

   }
}
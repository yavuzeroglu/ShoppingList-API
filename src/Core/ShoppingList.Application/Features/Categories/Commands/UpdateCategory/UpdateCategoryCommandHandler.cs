using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Categories;

namespace ShoppingList.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, Unit>
{
   private readonly ICategoryReadRepository _categoryReadRepository;
   private readonly ICategoryWriteRepository _categoryWriteRepository;

   public UpdateCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
   {
      _categoryReadRepository = categoryReadRepository;
      _categoryWriteRepository = categoryWriteRepository;
   }

   public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
   {
      var category = await _categoryReadRepository.GetByIdAsync(request.Id, false);

      if (category is null)
         throw new NotFoundException("Not Found Category");

      category.ParentCategoryId = request.ParentCategoryId ?? category.ParentCategoryId;
      category.Name = request.Name;

      _categoryWriteRepository.Update(category);
      await _categoryWriteRepository.SaveAsync();

      return Unit.Value;
   }
}
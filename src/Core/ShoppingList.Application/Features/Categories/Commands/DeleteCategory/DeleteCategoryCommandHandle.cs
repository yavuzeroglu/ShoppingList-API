using MediatR;
using SendGrid.Helpers.Errors.Model;
using ShoppingList.Application.Common.Abstractions.Repositories.Categories;

namespace ShoppingList.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandle : IRequestHandler<DeleteCategoryCommandRequest, Unit>
{
   private readonly ICategoryReadRepository _categoryReadRepository;
   private readonly ICategoryWriteRepository _categoryWriteRepository;

   public DeleteCategoryCommandHandle(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
   {
      _categoryReadRepository = categoryReadRepository;
      _categoryWriteRepository = categoryWriteRepository;
   }

   public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
   {
      var category = await _categoryReadRepository.GetSingleAsync(c => c.Id == request.Id);
      if (category is null)
         throw new NotFoundException("Category Not Found");

      _categoryWriteRepository.Remove(category);
      await _categoryWriteRepository.SaveAsync();

      return Unit.Value;
   }
}
using FluentValidation;

namespace ShoppingList.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommandRequest>
{
   public DeleteCategoryCommandValidator()
   {
      RuleFor(c => c.Id)
         .NotEmpty().WithMessage("Id boş olamaz")
         .GreaterThan(0).WithMessage("Lütfen geçerli bir ");
   }
}
using FluentValidation;

namespace ShoppingList.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandRequest>
{
   public CreateCategoryCommandValidator()
   {
      RuleFor(p => p.Name)
         .NotEmpty().WithMessage("Kategori adı boş bırakılamaz")
         .MinimumLength(3).MaximumLength(150).WithMessage("Kategori adı 3 ile 150 karakter arasında olmalıdır.");
   }
}
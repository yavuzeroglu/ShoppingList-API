using FluentValidation;

namespace ShoppingList.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommandRequest>
{
   public UpdateCategoryCommandValidator()
   {
      RuleFor(c => c.Id)
         .GreaterThan(0).WithMessage("Id kısmı boş olamaz lütfen ");

      RuleFor(c => c.ParentCategoryId)
         .Cascade(CascadeMode.Stop)
         .Must(id => !id.HasValue || id > 0)
         .WithMessage("Geçerli bir Üst Kategori seçiniz");

      RuleFor(c => c.Name)
         .NotEmpty().WithMessage("İsim boş olamaz")
         .Length(3, 150).WithMessage("İsim 3 ile 150 karakter arasında olmalıdır.");
   }
}
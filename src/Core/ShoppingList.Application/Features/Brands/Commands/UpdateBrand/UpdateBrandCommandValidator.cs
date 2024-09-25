using FluentValidation;

namespace ShoppingList.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommandRequest>
{
   public UpdateBrandCommandValidator()
   {
      RuleFor(b => b.Id)
         .NotEmpty().WithMessage("Id boş olamaz")
         .GreaterThan(0).WithMessage("Id 0(sıfır) olamaz");

      RuleFor(b => b.Name)
         .NotEmpty().WithMessage("Marka ismi boş bırakalamaz")
         .Length(2, 100).WithMessage("Marka ismi 2 ile 100 karakter arasında olmalıdır.");
   }
}
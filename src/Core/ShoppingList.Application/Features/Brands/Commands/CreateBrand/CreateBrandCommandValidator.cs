using FluentValidation;

namespace ShoppingList.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommandRequest>
{
   public CreateBrandCommandValidator()
   {
      RuleFor(b => b.Name)
         .NotEmpty().NotNull().WithMessage("Marka ismi boş bırakalamaz")
         .Length(2, 100).WithMessage("Marka ismi 2 ile 100 karakter arasında olmalı.");
   }
}
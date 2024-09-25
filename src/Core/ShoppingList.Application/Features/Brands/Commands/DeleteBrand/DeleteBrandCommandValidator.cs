using FluentValidation;

namespace ShoppingList.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommandRequest>
{
   public DeleteBrandCommandValidator()
   {
      RuleFor(b => b.Id)
         .NotEmpty().NotNull().WithMessage("Marka boş olamaz, lütfen marka seçiniz.");
      RuleFor(b => b.Id)
         .GreaterThan(0).WithMessage("Geçerli bir marka seçiniz");
   }
}
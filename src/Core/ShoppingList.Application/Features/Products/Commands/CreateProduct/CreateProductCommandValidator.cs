using FluentValidation;
using FluentValidation.Results;

namespace ShoppingList.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
{
   public CreateProductCommandValidator()
   {
      RuleFor(p => p.Name)
         .NotEmpty().NotNull()
            .WithMessage("Lütfen ürün adını boş bırakmayınız")
         .MinimumLength(2).MaximumLength(150)
            .WithMessage("Ürün adı 2 ile 150 olmalı.");


      RuleFor(p => p.BrandId)
         .GreaterThan(0)
            .WithMessage("Lütfen bir marka seçiniz");

      RuleFor(p => p.CategoryId)
         .GreaterThan(0)
            .WithMessage("Lütfen bir kategori seçiniz");

      RuleFor(p => p.CreatedDate)
         .Custom((createdDate, context) =>
         {
            if (createdDate.Date != DateTime.UtcNow.Date ||
               createdDate.Date < DateTime.UtcNow.Date)
            {
               context.AddFailure(new ValidationFailure("DateTime fail", "The creation date of the product cannot be earlier than today"));
            }
         });


   }
}
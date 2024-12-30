using FluentValidation;
using FluentValidation.Results;

namespace ShoppingList.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
{
   public CreateProductCommandValidator()
   {
      RuleFor(p => p.Name)
         .NotEmpty().NotNull()
            .WithName("Ürün Adı")
            .WithMessage("Lütfen ürün adını boş bırakmayınız");

      RuleFor(p => p.Name)
         .MinimumLength(2).MaximumLength(150)
         .WithName("Ürün Adı")
            .WithMessage("Ürün 2 ile 150 olmalı.");

      RuleFor(p => p.BrandId)
         .GreaterThan(0)
            .WithMessage("Lütfen bir marka seçiniz");

      RuleFor(p => p.CategoryId)
         .GreaterThan(0)
            .WithMessage("Lütfen bir kategori seçiniz");

      RuleFor(p => p.Unit)
         .NotEmpty().NotNull()
            .WithName("Birim")
            .WithMessage("Lütfen ürün birimini boş bırakmayınız");

      RuleFor(p => p.Unit)
         .MinimumLength(1).MaximumLength(20)
            .WithName("Birim")
            .WithMessage("Birim 1 ile 20 karakter arasında olmalıdır");

      RuleFor(p => p.EstimatedPrice)
         .NotEmpty().NotNull()
            .WithName("Tahmini Fiyat")
            .WithMessage("Lütfen tahmini fiyatı boş bırakmayınız");

      RuleFor(x => x.EstimatedPrice)
         .GreaterThan(0)
            .WithMessage("Tahmini fiyat 0'dan büyük olmalıdır")
         .PrecisionScale(18, 2, false)
            .WithMessage("Tahmini fiyat en fazla 2 ondalık basamak içerebilir.");


      RuleFor(x => x)
            .Must(x => x.EstimatedPrice <= 500000)
            .WithMessage("Tahmini fiyat 500.000'dan büyük olamaz.");

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
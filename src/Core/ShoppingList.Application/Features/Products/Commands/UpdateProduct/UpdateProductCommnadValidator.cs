using FluentValidation;

namespace ShoppingList.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommnadValidator : AbstractValidator<UpdateProductCommandRequest>
{
    public UpdateProductCommnadValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0)
                .WithMessage("ID 1 den küçük olamaz");

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
    }
}

using FluentValidation;
using ShoppingList.Domain.Enums;

namespace ShoppingList.Application.Features.Products.Commands.PatchProduct;

public class PatchProductCommandValidator : AbstractValidator<PatchProductCommandRequest>
{
    public PatchProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("ID 1'den küçük olamaz");

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name)
                .MinimumLength(2)
                .MaximumLength(150)
                .WithMessage("Ürün adı 2 ile 150 karakter arasında olmalıdır");
        });

        When(x => x.CategoryId.HasValue, () =>
        {
            RuleFor(x => x.CategoryId.Value)
                .GreaterThan(0)
                .WithMessage("Geçerli bir kategori ID'si girilmelidir");
        });

        When(x => x.BrandId.HasValue, () =>
        {
            RuleFor(x => x.BrandId.Value)
                .GreaterThan(0)
                .WithMessage("Geçerli bir marka ID'si girilmelidir");
        });

        When(x => x.EstimatedPrice > 0, () =>
        {
            RuleFor(x => x.EstimatedPrice)
                .GreaterThan(0)
                .WithMessage("Tahmini fiyat 0'dan büyük olmalıdır");
        });

        When(x => !string.IsNullOrEmpty(x.Unit), () =>
        {
            RuleFor(x => x.Unit)
                .Must(unit => Enum.TryParse<UnitType>(unit, true, out _))
                .WithMessage(x => $"Geçersiz birim tipi. Geçerli değerler: {string.Join(", ", Enum.GetNames<UnitType>())}");
        });
    }
}
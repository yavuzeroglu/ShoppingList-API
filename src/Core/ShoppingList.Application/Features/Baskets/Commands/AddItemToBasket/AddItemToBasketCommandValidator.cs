using FluentValidation;
using ShoppingList.Application.Features.Baskets.Commands.ADdItemToBasket;

namespace ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;

public class AddItemToBasketCommandValidator : AbstractValidator<AddItemToBasketCommandRequest>
{
    public AddItemToBasketCommandValidator()
    {
        RuleFor(x => x.BasketId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Sepet ID'si geçerli bir değer olmalıdır.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Ürün ID'si geçerli bir değer olmalıdır.");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Miktar 0'dan büyük olmalıdır.");

        RuleFor(x => x.UnitPrice)
            .NotEmpty()
            .GreaterThan(0)
            .PrecisionScale(18, 2, false)
            .WithMessage("Birim fiyat 0'dan büyük olmalı ve en fazla 2 ondalık basamak içermelidir.");

        // Kompleks kural örneği
        RuleFor(x => x)
            .Must(x => x.UnitPrice * x.Quantity <= 1000000)
            .WithMessage("Toplam tutar 1.000.000'dan büyük olamaz.");
    }
}
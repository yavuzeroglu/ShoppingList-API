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
    }
}
using FluentValidation;
using ShoppingList.Application.Common.Abstractions.Services;

namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasket;

public class RemoveBasketCommandValidator : AbstractValidator<RemoveBasketCommandRequest>
{
    private readonly IBasketService _basketService;

    public RemoveBasketCommandValidator(IBasketService basketService)
    {
        _basketService = basketService;

        RuleFor(x => x.BasketId)
            .NotEmpty()
            .WithMessage("Sepet ID'si boş olamaz.")
            .GreaterThan(0)
            .WithMessage("Geçersiz sepet ID'si.")
            .MustAsync(async (id, cancellation) =>
            {
                return await _basketService.IsBasketEmptyAsync(id);
            })
            .WithMessage("İçinde ürün bulunan sepet silinemez.");
    }
}
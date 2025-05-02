using FluentValidation;
using ShoppingList.Application.Common.Abstractions.Services;

namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasket;

public class RemoveBasketCommandValidator : AbstractValidator<RemoveBasketCommandRequest>
{
    private readonly IBasketItemService _basketItemService;

    public RemoveBasketCommandValidator(IBasketItemService basketItemService)
    {
        
        _basketItemService = basketItemService;

        RuleFor(x => x.BasketId)
            .NotEmpty()
            .WithMessage("Sepet ID'si boş olamaz.")
            .GreaterThan(0)
            .WithMessage("Geçersiz sepet ID'si.")
            .MustAsync(async (id, cancellation) =>
            {
                return await _basketItemService.IsBasketItemExistsAsync(id);
            })
            .WithMessage("İçinde ürün bulunan sepet silinemez.");
    }
}
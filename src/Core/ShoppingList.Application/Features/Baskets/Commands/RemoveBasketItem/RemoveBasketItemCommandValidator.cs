using FluentValidation;
using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;

namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommandValidator : AbstractValidator<RemoveBasketItemCommandRequest>
{
    private readonly IBasketItemReadRepository _basketItemReadRepository;

    public RemoveBasketItemCommandValidator(IBasketItemReadRepository basketItemReadRepository)
    {
        _basketItemReadRepository = basketItemReadRepository;

        RuleFor(x => x.BasketItemId)
            .NotEmpty()
            .WithMessage("Sepet ürün ID'si boş olamaz.")
            .GreaterThan(0)
            .WithMessage("Geçersiz sepet ürün ID'si.")
            .MustAsync(async (id, cancellation) =>
            {
                return await _basketItemReadRepository.AnyAsync(bi => bi.Id == id);
            })
            .WithMessage("Belirtilen sepet ürünü bulunamadı.");
    }
}
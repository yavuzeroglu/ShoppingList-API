using FluentValidation;
using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;

namespace ShoppingList.Application.Features.Baskets.Commands.UpdateQuantityBasketItem;

public class UpdateQuantityBasketItemCommandValidator : AbstractValidator<UpdateQuantityBasketItemCommandRequest>
{
    private readonly IBasketItemReadRepository _basketItemReadRepository;

    public UpdateQuantityBasketItemCommandValidator(IBasketItemReadRepository basketItemReadRepository)
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

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Miktar boş olamaz.")
            .GreaterThan(0)
            .WithMessage("Miktar 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100)
            .WithMessage("Miktar 100'den fazla olamaz.");
    }
}

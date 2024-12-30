using FluentValidation;
using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;

namespace ShoppingList.Application.Features.Baskets.Commands.CreateBasket;

    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommandRequest>
{
    private readonly IBasketReadRepository _basketReadRepository;

    public CreateBasketCommandValidator(IBasketReadRepository basketReadRepository)
    {
        _basketReadRepository = basketReadRepository;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Sepet adı boş olamaz.")
            .MinimumLength(3)
            .WithMessage("Sepet adı en az 3 karakter olmalıdır.")
            .MaximumLength(50)
            .WithMessage("Sepet adı en fazla 50 karakter olabilir.")
            .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ0-9 ]*$")
            .WithMessage("Sepet adı sadece harf, rakam ve boşluk içerebilir.")
            .MustAsync(async (name, cancellation) =>
            {
                // Aynı kullanıcının aynı isimde sepeti var mı kontrolü
                bool exists = await _basketReadRepository
                    .AnyAsync(b => b.Name == name && !b.IsPurchased);
                return !exists;
            })
            .WithMessage("Bu isimde aktif bir sepetiniz zaten var.");

        // Boşluk kontrolü
        RuleFor(x => x.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name) && name.Trim() == name)
            .WithMessage("Sepet adı başında veya sonunda boşluk içeremez.");
    }
}

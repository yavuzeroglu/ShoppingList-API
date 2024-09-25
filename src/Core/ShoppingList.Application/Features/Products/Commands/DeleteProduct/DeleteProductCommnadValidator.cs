
using FluentValidation;

namespace ShoppingList.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommnadValidator : AbstractValidator<DeleteProductCommandRequest>
{
    public DeleteProductCommnadValidator()
    {
        RuleFor(p => p.Id)
                .GreaterThan(0)
                    .WithMessage("ID 1 den küçük olamaz");
    }
}

using FluentValidation;

namespace ShoppingList.Application.Features.Products.Queries.GetByIdProduct;


public class GetByIdProductQueryValidator : AbstractValidator<GetByIdProductQueryRequest>
{
   public GetByIdProductQueryValidator()
   {
      RuleFor(p => p.Id)
         .GreaterThan(0)
             .WithMessage("Ürün kısmı boş bırakılamaz");
   }
}
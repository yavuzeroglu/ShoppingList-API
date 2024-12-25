using ShoppingList.Application.Common.Abstractions.Repositories.ProductImage;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.ProductImage;

public class ProductImageReadRepository : ReadRepository<Image>, IProductImageReadRepository
{
   public ProductImageReadRepository(ShoppingListDbContext context) : base(context)
   { }
}

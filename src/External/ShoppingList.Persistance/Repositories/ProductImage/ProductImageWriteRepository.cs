using ShoppingList.Application.Common.Abstractions.Repositories.ProductImage;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.ProductImage;

public class ProductImageWriteRepository : WriteRepository<Image>, IProductImageWriteRepository
{
   public ProductImageWriteRepository(ShoppingListDbContext context) : base(context)
   { }
}
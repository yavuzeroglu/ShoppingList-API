using ShoppingList.Application.Repositories.Brands;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Brands;

public class BrandReadRepository : ReadRepository<Brand>, IBrandReadRepository
{
   public BrandReadRepository(ShoppingListDbContext context) : base(context)
   { }
}
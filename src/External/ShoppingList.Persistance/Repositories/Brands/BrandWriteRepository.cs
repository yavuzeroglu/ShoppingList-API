using ShoppingList.Application.Repositories.Brands;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Brands;

public class BrandWriteRepository : WriteRepository<Brand>, IBrandWriteRepository
{
   public BrandWriteRepository(ShoppingListDbContext context) : base(context)
   { }
}
using ShoppingList.Application.Common.Abstractions.Repositories.Products;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Products;


public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
   public ProductWriteRepository(ShoppingListDbContext context) : base(context)
   { }
}
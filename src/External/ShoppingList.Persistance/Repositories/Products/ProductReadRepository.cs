using ShoppingList.Application.Abstractions.Repositories.Products;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Products;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(ShoppingListDbContext context) : base(context)
    { }
}
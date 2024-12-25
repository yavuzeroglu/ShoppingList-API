using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Baskets;

public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
{
    public BasketWriteRepository(ShoppingListDbContext context) : base(context)
    {
    }
}
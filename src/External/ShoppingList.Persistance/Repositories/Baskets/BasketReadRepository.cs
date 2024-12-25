using ShoppingList.Application.Common.Abstractions.Repositories.Baskets;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Baskets;

public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
{
    public BasketReadRepository(ShoppingListDbContext context) : base(context)
    {
    }
}

using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.BasketLines;

public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
{
    public BasketItemReadRepository(ShoppingListDbContext context) : base(context)
    { }
}

using ShoppingList.Application.Common.Abstractions.Repositories.BasketItems;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.BasketLines;

public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
{
    public BasketItemWriteRepository(ShoppingListDbContext context) : base(context)
    {
    }
}
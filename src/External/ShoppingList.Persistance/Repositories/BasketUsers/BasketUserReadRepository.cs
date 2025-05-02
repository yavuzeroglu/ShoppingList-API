using ShoppingList.Application.Common.Abstractions.Repositories.BasketUsers;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.BasketUsers;

public class BasketUserReadRepository : ReadRepository<BasketUser>, IBasketUserReadRepository
{
    public BasketUserReadRepository(ShoppingListDbContext context) : base(context)
    { }
}

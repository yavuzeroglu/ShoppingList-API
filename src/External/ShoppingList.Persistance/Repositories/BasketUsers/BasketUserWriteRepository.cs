using ShoppingList.Application.Common.Abstractions.Repositories.BasketUsers;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.BasketUsers;

public class BasketUserWriteRepository : WriteRepository<BasketUser>, IBasketUserWriteRepository
{
    public BasketUserWriteRepository(ShoppingListDbContext context) : base(context)
    { }
}

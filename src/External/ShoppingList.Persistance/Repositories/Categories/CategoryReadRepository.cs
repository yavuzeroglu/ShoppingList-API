using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Categories;

public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
{
    public CategoryReadRepository(ShoppingListDbContext context) : base(context)
    { }
}
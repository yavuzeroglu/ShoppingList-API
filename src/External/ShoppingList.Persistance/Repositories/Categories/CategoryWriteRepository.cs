using ShoppingList.Application.Abstractions.Repositories.Categories;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories.Categories;

public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(ShoppingListDbContext context) : base(context)
    { }
}
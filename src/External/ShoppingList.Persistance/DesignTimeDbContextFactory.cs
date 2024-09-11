using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShoppingListDbContext>
{
   public ShoppingListDbContext CreateDbContext(string[] args)
   {
      DbContextOptionsBuilder<ShoppingListDbContext> dbContextOptionsBuilder = new();
      dbContextOptionsBuilder.UseNpgsql(ConfigurationHelper.GetConnectionString);
      return new(dbContextOptionsBuilder.Options);
   }
}
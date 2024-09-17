using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShoppingListDbContext>
{
   public ShoppingListDbContext CreateDbContext(string[] args)
   {
      var configuration = new ConfigurationBuilder()
         .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../ShoppingList.WebAPI"))
         .AddJsonFile("appsettings.json")
         .Build();

      var builder = new DbContextOptionsBuilder<ShoppingListDbContext>()
         .UseNpgsql(configuration.GetConnectionString("PostgreSQL"),
         prj => prj.MigrationsAssembly("ShoppingList.Persistance"));

      return new ShoppingListDbContext(builder.Options);
   }
}
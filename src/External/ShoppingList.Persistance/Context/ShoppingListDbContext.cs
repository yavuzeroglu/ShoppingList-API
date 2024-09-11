using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Persistance.Context;


public class ShoppingListDbContext : DbContext
{
    public ShoppingListDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
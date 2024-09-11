using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Persistance.Context.Confiugrations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product() { Id = 1, Name = "Portakal", CategoryId = 7 },
            new Product() { Id = 2, Name = "Greyfurt", CategoryId = 7 },
            new Product() { Id = 3, Name = "Kavun", CategoryId = 6 },
            new Product() { Id = 4, Name = "Karpuz", CategoryId = 6 }
        );
    }
}
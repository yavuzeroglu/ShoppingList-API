using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Persistance.Context.Confiugrations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder.HasOne(i => i.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(i => i.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Product() { Id = 1, Name = "Portakal", CategoryId = 7, BrandId = 1 },
            new Product() { Id = 2, Name = "Greyfurt", CategoryId = 7, BrandId = 1 },
            new Product() { Id = 3, Name = "Kavun", CategoryId = 6, BrandId = 1 },
            new Product() { Id = 4, Name = "Karpuz", CategoryId = 6, BrandId = 1 },
            new Product() { Id = 5, Name = "Tarhana", CategoryId = 1, BrandId = 1 }
        );
    }
}
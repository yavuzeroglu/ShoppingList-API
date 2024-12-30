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
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Persistance.Context.Confiugrations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasMany(b => b.Products)
         .WithOne(p => p.Brand)
         .HasForeignKey(p => p.BrandId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
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
        // DeleteBehavior.Cascade : Bir Brand silindiginde, ilişkili tüm Product kayitlari silinir.

        builder.HasData(
           new Brand() { Id = 1, CreatedDate = DateTime.UtcNow, Name = "TEST", },
           new Brand() { Id = 2, CreatedDate = DateTime.UtcNow, Name = "Reyoncunuz" },
           new Brand() { Id = 3, CreatedDate = DateTime.UtcNow, Name = "ETİ" }
        );
    }
}
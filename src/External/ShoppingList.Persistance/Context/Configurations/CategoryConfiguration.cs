using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Persistance.Context.Confiugrations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.ParentCategory)
               .WithMany(c => c.SubCategories)
               .HasForeignKey(c => c.ParentCategoryId);

        builder.HasMany(c => c.Products)
               .WithOne(c => c.Category)
               .HasForeignKey(c => c.CategoryId);


        builder.HasData(
            new Category() { Id = 1, Name = "Temel Gıda" },
            new Category() { Id = 2, Name = "Meyve Sebze" },
            new Category() { Id = 3, Name = "Meyve", ParentCategoryId = 2 },
            new Category() { Id = 4, Name = "Doğranmış, Ayıklanmış Meyveler", ParentCategoryId = 3 },
            new Category() { Id = 5, Name = "Egzotik Meyveler", ParentCategoryId = 3 },
            new Category() { Id = 6, Name = "Kavun ve Karpuz", ParentCategoryId = 3 },
            new Category() { Id = 7, Name = "Narenciye", ParentCategoryId = 3 }
        );
    }
}
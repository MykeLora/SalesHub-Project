using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Tabla
            builder.ToTable("Products");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Id
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            // SKU
            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(50);

            // Name
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            // Description
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

            // Price
            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            // Stock
            builder.Property(p => p.Stock)
                .IsRequired();

            // MinimumStock
            builder.Property(p => p.MinimumStock)
                .IsRequired();

            // Status
            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            // CategoryId
            builder.Property(p => p.CategoryId)
                .IsRequired();

            // Unique SKU
            builder.HasIndex(p => p.SKU)
                .IsUnique();

            // Category Index
            builder.HasIndex(p => p.CategoryId);

            // Product N:1 Category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product 1:N SaleDetail
            builder.HasMany(p => p.SaleDetails)
                .WithOne(sd => sd.Product)
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product 1:N InventoryMovement
            builder.HasMany(p => p.InventoryMovements)
                .WithOne(im => im.Product)
                .HasForeignKey(im => im.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

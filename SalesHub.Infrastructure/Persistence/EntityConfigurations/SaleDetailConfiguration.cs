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
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            // Tabla
            builder.ToTable("SaleDetails");

            // Primary Key
            builder.HasKey(sd => sd.Id);

            // Id
            builder.Property(sd => sd.Id)
                .ValueGeneratedOnAdd();

            // SaleId
            builder.Property(sd => sd.SaleId)
                .IsRequired();

            
            builder.Property(sd => sd.ProductId)
                .IsRequired();

            builder.Property(sd => sd.Quantity)
                .IsRequired();

           
            builder.Property(sd => sd.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            
            builder.Property(sd => sd.SubTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            
            builder.HasOne(sd => sd.Sale)
                .WithMany(s => s.Details)
                .HasForeignKey(sd => sd.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(sd => sd.Product)
                .WithMany(p => p.SaleDetails)
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(sd => sd.SaleId);

            builder.HasIndex(sd => sd.ProductId);
        }
    }
}

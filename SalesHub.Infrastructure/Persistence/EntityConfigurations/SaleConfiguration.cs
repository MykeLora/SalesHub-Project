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
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            
            builder.ToTable("Sales");

            
            builder.HasKey(s => s.Id);

            
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            
            builder.Property(s => s.CustomerId)
                .IsRequired();

            builder.Property(s => s.UserId)
                .IsRequired();

         
            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.SubTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.Tax)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.Discount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.Total)
                .IsRequired()
                .HasPrecision(18, 2);

   
            builder.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Details)
                .WithOne(sd => sd.Sale)
                .HasForeignKey(sd => sd.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

        
            builder.HasIndex(s => s.CustomerId);

            builder.HasIndex(s => s.UserId);

            builder.HasIndex(s => s.SaleDate);
        }
    }
}

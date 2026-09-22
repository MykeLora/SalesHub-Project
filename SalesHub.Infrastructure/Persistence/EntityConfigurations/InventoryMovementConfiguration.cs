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
    public class InventoryMovementConfiguration
        : IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            
            builder.ToTable("InventoryMovements");

            
            builder.HasKey(im => im.Id);

         
            builder.Property(im => im.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(im => im.ProductId)
                .IsRequired();

          
            builder.Property(im => im.UserId)
                .IsRequired();

            
            builder.Property(im => im.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            
            builder.Property(im => im.Quantity)
                .IsRequired();

            builder.Property(im => im.Reason)
                .IsRequired()
                .HasMaxLength(500);


            builder.HasOne(im => im.Product)
                .WithMany(p => p.InventoryMovements)
                .HasForeignKey(im => im.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(im => im.User)
                .WithMany(u => u.InventoryMovements)
                .HasForeignKey(im => im.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(im => im.ProductId);

            builder.HasIndex(im => im.UserId);

            builder.HasIndex(im => im.Created);
        }
    }
}

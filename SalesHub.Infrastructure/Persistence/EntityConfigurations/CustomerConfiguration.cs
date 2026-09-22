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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.ToTable("Customers");

            
            builder.HasKey(c => c.Id);

           
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(254);

            
            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.IsActive)
                .IsRequired();

            
            builder.HasIndex(c => c.Email)
                .IsUnique();

            
            builder.HasMany(c => c.Sales)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

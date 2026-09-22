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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           
            builder.ToTable("Categories");

            
            builder.HasKey(c => c.Id);

          
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

          
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            
            builder.Property(c => c.IsActive)
                .IsRequired();


            
            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

          
            builder.HasIndex(c => c.Name);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Infrastructure.Persistence.Context
{
    public class SalesHubDbContext : DbContext
    {
        public SalesHubDbContext(DbContextOptions<SalesHubDbContext> options)
            : base(options)
        {         
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();
        public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SalesHubDbContext).Assembly);
        }

    }
}

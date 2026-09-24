using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface;
using SalesHub.Domain.Entities;
using SalesHub.Domain.Enums;
using SalesHub.Infrastructure.Persistence;
using SalesHub.Infrastructure.Persistence.Context;
using SalesHub.Infrastructure.Persistence.Repositories;

namespace SalesHub.Infrastructure.Repositories
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(SalesHubDbContext context)
        : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Status == ProductStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product?> GetByCodeAsync(string sku)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.SKU == sku);
        }
    }
}

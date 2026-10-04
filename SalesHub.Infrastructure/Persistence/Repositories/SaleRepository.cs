using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Domain.Entities;
using SalesHub.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(SalesHubDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sale>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Sales
               .Include(s => s.Details)
                   .ThenInclude(d => d.Product)
               .AsNoTracking()
               .Where(s => s.CustomerId == customerId)
               .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetByUserIdAsync(int userId)
        {
            return await _context.Sales
                 .Include(s => s.Details)
                     .ThenInclude(d => d.Product)
                 .AsNoTracking()
                 .Where(s => s.UserId == userId)
                 .ToListAsync();
        }

        public async Task<Sale?> GetWithDetailsAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Details)
                    .ThenInclude(d => d.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sale>> GetAllWithDetailsAsync()
        {
            return await _context.Sales
                .Include(s => s.Details)
                    .ThenInclude(d => d.Product)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

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
    public class InventoryMovementRepository : GenericRepository<InventoryMovement>,IInventoryMovementRepository
    {
        public InventoryMovementRepository(SalesHubDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<InventoryMovement>> GetInventoryByProductIdAsync(int productId)
        {
            return await _context.InventoryMovements
                .Include(im => im.Product)
                .Include(im => im.User)
                .AsNoTracking()
                .Where(im => im.ProductId == productId)
                .ToListAsync();
                
        }

        public async Task<IEnumerable<InventoryMovement>> GetInventoryByUserIdAsync(int userId)
        {
            return await _context.InventoryMovements
                .Include(m => m.Product)
                .Include(m => m.User)
                .AsNoTracking()
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        public async Task<InventoryMovement?> GetLastByProductIdAsync(int productId)
        {
            return await _context.InventoryMovements
                .Where(m => m.ProductId == productId)
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();
        }
    }
}
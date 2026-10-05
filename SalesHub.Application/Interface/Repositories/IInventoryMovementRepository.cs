using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Repositories
{
    public interface IInventoryMovementRepository : IGenericRepository<InventoryMovement>
    {
        Task<IEnumerable<InventoryMovement>> GetInventoryByProductIdAsync(int productId);
        Task<IEnumerable<InventoryMovement>> GetInventoryByUserIdAsync(int userId);
        Task<InventoryMovement?> GetLastByProductIdAsync(int productId);
    }
}

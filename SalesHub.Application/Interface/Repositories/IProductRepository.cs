using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetByCodeAsync(string name);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetActiveProductsAsync();
    }
}

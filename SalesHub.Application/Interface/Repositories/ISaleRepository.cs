using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Repositories
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<IEnumerable<Sale>> GetByCustomerIdAsync(int customerId);

        Task<IEnumerable<Sale>> GetByUserIdAsync(int userId);

        Task<Sale?> GetWithDetailsAsync(int id);
        Task<IEnumerable<Sale>> GetAllWithDetailsAsync();
    }
}

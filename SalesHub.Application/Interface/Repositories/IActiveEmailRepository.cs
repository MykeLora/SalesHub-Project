using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Repositories
{
    public interface IActiveEmailRepository<T>
        where T : class
    {
        Task<T?> GetByEmailAsync(string email);

        Task<IEnumerable<T>> GetActiveAsync();
    }
}

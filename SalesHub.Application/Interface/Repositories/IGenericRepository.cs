using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task Update(T entity);

        Task<bool> DeleteAsync(int id);

        Task<List<T>> GetAllWithIncludes(List<string> properties);
    }
}

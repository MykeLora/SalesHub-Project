using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly SalesHubDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SalesHubDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
           return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public  void Update(T entity)
        {
             _dbSet.Update(entity);
        }
    }
}

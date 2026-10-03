using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Infrastructure.Persistence.Repositories
{
    public abstract class ActiveEmailRepository<T>
        : GenericRepository<T>, IActiveEmailRepository<T>
        where T : class
    {
        protected ActiveEmailRepository(SalesHubDbContext context)
            : base(context)
        {
        }

        public async Task<T?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e =>
                    EF.Property<string>(e, "Email") == email);
        }

        public async Task<IEnumerable<T>> GetActiveAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(e =>
                    EF.Property<bool>(e, "IsActive"))
                .ToListAsync();
        }
    }
}

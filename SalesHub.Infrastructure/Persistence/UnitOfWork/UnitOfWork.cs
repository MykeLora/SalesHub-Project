using SalesHub.Application.Interface;
using SalesHub.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly SalesHubDbContext _context;

        public UnitOfWork(SalesHubDbContext context)
        {
            _context = context;

        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using SalesHub.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SalesHubDbContext context)
            : base(context)
        {
        }

        public async Task<Customer?> GetByNationalIdAsync(string nationalId)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.NationalId == nationalId);
        }
    }
}

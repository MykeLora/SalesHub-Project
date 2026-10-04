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
    public class SaleDetailRepository : GenericRepository<SaleDetail>, ISaleDetailRepository
    {
        public SaleDetailRepository(SalesHubDbContext context)
            : base(context)
        {
        }
    }
}

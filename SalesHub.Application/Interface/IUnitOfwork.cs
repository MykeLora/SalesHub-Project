using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface
{
    public interface IUnitOfwork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

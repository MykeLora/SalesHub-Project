using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services.Sale
{
    public class SaleNumberGenerator : ISaleNumberGenerator
    {
        public string GenerateAsync()
        {
            return $"SAL-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }

    }
}

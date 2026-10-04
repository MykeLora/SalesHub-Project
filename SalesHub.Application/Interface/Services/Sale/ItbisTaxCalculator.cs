using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services.Sale
{
    public class ItbisTaxCalculator : ITaxCalculator
    {
        private const decimal ItbisRate = 0.18m;
        public decimal Calculate(decimal taxableAmount)
        {
            return taxableAmount * ItbisRate;
        }
    }
}

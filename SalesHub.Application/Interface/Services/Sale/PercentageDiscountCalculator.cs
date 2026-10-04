using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services.Sale
{
    public class PercentageDiscountCalculator : IDiscountCalculator
    {
        public decimal CalculateDiscount(decimal subTotal, decimal discountPercentage)
        {
            return subTotal * discountPercentage / 100;
        }
    }
}

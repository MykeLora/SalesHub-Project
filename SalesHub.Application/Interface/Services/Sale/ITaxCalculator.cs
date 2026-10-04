using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services.Sale
{
    public interface ITaxCalculator
    {
        decimal Calculate(decimal taxableAmount);
    }
}

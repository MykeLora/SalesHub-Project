using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.DTOs.Sale.SaleDetail
{
    public class CreateSaleDetailDto 
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}

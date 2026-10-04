using SalesHub.Application.DTOs.Sale.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.DTOs.Sale
{
    public class CreateSaleDto
    {
        public int CustomerId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public List<CreateSaleDetailDto> Details { get; set; } = new List<CreateSaleDetailDto>();
    }
}

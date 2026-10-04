using SalesHub.Application.DTOs.Sale.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.DTOs.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }

        public string SaleNumber { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; } 
        public int CustomerId { get; set; }
        public int UserId { get; set; }

        public decimal SubTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public List<SaleDetailDto> Details { get; set; } = new();
    }
}

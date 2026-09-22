using SalesHub.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Domain.Entities
{
    public class SaleDetail : BaseEntity
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Sale? Sale { get; set; }
        public virtual Product? Product { get; set; }
    }
}

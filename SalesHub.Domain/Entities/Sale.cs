using SalesHub.Domain.Commons;
using SalesHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public DateTime SaleDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public int CustomerId { get; set; }
        public int UserId { get; set; }
        
        public virtual Customer? Customer { get; set; }
        public virtual User? User { get; set; }

        public ICollection<SaleDetail> Details { get; set; }
        = new List<SaleDetail>();
    }
}

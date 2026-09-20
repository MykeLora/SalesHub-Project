using SalesHub.Domain.Commons;
using SalesHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price {  get; set; }
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
        public ProductStatus Status { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
        = new List<SaleDetail>();
        public ICollection<InventoryMovement> InventoryMovements { get; set; }
        = new List<InventoryMovement>();

    }
}

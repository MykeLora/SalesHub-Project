using SalesHub.Domain.Commons;
using SalesHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Domain.Entities
{
    public class InventoryMovement : BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public InventoryMovementType Type {  get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; } = string.Empty;

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
        
    }
}

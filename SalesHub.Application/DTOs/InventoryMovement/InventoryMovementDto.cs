using SalesHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.DTOs.InventoryMovement
{
    public class InventoryMovementDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public InventoryMovementType Type { get; set; }

        public int Quantity { get; set; }

        public int PreviousStock { get; set; }

        public int ResultingStock { get; set; }

        public string Reason { get; set; } = string.Empty;
    }

}

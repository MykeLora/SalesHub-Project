using SalesHub.Domain.Commons;
using SalesHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<InventoryMovement> InventoryMovements { get; set; }
        = new List<InventoryMovement>();

    }
}

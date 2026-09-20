using SalesHub.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();


    }
}

using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Products : BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}

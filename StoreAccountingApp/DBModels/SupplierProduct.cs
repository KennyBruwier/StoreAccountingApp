using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class SupplierProduct
    {
        [Key, Column(Order = 0)]
        public int SupplierId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }
        [MaxLength(20)]
        public string Status { get; set; }
        [Required]
        public double UnitPrice { get; set; } = 0;
        public float Discount { get; set; } = 0;
    }
}

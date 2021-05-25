using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.Models.Abstracts;

namespace StoreAccountingApp.Models
{
    public class Product : RecordTimeStamps
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }
    }
}

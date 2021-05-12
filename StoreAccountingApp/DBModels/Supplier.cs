using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class Supplier : Credentials
    {
        [Key]
        public int SupplierId { get; set; }
        [MaxLength(30)]
        public string Organisation { get; set; }
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

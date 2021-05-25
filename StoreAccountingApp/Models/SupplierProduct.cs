using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.Models.Abstracts;

namespace StoreAccountingApp.Models
{
    public class SupplierProduct : ProductRecord
    {
        [Key, Column(Order = 0)]
        public int SupplierId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }
    }
}

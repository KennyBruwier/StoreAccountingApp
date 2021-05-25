using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class SaleProduct : ItemTransaction
    {
        [Key, Column(Order = 0)]
        public int SaleId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
    }
}

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
    public class OrderProduct : ItemTransaction
    {
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

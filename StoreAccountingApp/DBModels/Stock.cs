using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class Stock
    {
        [Key, Column(Order = 0)]
        public int StoreId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
        [MaxLength(20)]
        public string Category { get; set; }
        [Required]
        public int Amount { get; set; } = 0;
        [Required]
        public int MinAmount { get; set; } = 0;
        [Required]
        public int MaxAmount { get; set; } = 0;
        [Required]
        public double UnitPrice { get; set; }
    }
}

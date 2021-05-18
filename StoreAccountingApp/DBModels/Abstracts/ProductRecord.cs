using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public class ProductRecord:RecordTimeStamps
    {
        [MaxLength(20)]
        public string Barcode { get; set; }
        [Required]
        public int Amount { get; set; } = 0;
        [Required]
        public int MinAmount { get; set; } = 0;
        [Required]
        public int MaxAmount { get; set; } = 0;
        [Required]
        public double UnitPrice { get; set; }
        [Required]
        public float Discount { get; set; } = 0;
        [Required]
        public float Guarantee { get; set; } = 0;
    }
}

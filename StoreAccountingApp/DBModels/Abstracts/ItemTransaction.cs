using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public abstract class ItemTransaction : RecordTimeStamps
    {
        [Required]
        public int Amount { get; set; } = 0;
        [MaxLength(20)]
        [Required]
        public string Status { get; set; } = "Active";
        [Required]
        public float UnitPrice { get; set; } = 0;
        [Required]
        public float VAT { get; set; } = 0;
        [Required]
        public float Discount { get; set; } = 0;
        [Required]
        public float Guarantee { get; set; } = 0;
    }
}

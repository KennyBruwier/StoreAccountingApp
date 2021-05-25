using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models.Abstracts
{
    public abstract class ItemTransaction : ProductRecord
    {
        [MaxLength(20)]
        [Required]
        public string Status { get; set; } = "Active";
        [Required]
        public float VAT { get; set; } = 0;
    }
}

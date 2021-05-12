using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public class Invoice : RecordTimeStamps
    {
        [Required]
        [MaxLength(20)]
        public string InvoiceNumber { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
        [Required]
        public float Guarantee { get; set; } = 0;
        [Required]
        public float Discount { get; set; } = 0;
        public string PaymentMethod { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public DateTime? ExpirationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}

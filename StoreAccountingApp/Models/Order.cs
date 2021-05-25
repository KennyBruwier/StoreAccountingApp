using StoreAccountingApp.Models.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreAccountingApp.Models
{
    public class Order : Invoice
    {
        [Key]
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int StoreId { get; set; }
        public virtual Shop Shop { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
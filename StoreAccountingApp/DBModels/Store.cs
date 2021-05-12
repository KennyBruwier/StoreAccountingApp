using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class Store : Credentials
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; }
        [MaxLength(9)]
        public string DUNSNumber { get; set; }
        [Required]
        public DateTime InService { get; set; } = DateTime.Now;
        [Required]
        public DateTime OutOfService { get; set; } = DateTime.Now;
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}

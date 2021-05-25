using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class Shop : ServiceBuildingCredentials
    {
        [Key]
        public int ShopId { get; set; }
        [MaxLength(9)]
        public string DUNSNumber { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}

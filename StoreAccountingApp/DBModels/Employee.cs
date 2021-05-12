using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class Employee : Credentials
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Active";
        [Required]
        public DateTime InService { get; set; } = DateTime.Now;
        [Required]
        public DateTime OutOfService { get; set; } = DateTime.Now;
        public virtual ICollection<JobFunction> JobFunctions { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

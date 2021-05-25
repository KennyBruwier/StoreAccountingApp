using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class Employee : ServicePersonCredentials
    {
        [Key]
        public int EmployeeId { get; set; }
        public virtual ICollection<JobFunction> JobFunctions { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Employee()
        {

        }
        public Employee(string firstname, string lastname, string email) 
        {
            Firstname = firstname;
            Lastname = lastname;
            EmailAddress = email;
        }

    }
}

using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class AccountType : RecordTimeStamps
    {
        [Key]
        public int AccountTypeId { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Admin { get; set; } = false;
        public bool StockManager { get; set; } = false;
        public bool Seller { get; set; } = false;
        public virtual ICollection<Account> Accounts { get; set; }
        public AccountType(string name)
        {
            Name = name;
        }
        public AccountType()
        {

        }

        public AccountType(string name, string description) : this(name)
        {
            Description = description;
        }
    }
}

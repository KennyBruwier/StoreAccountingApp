using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class AccountType 
    {
        [Key]
        public int AccountTypeId { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
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

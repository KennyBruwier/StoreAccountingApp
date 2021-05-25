using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class Account : AddressDigitalShort
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [MaxLength(30)]
        [Required]
        public string Username { get; set; }
        [MaxLength(50)]
        [Required]
        public string Password { get; set; }
        public Account(string username, string password, string email)
        {
            Username = username;
            Password = password;
            EmailAddress = email;
        }
        public Account()
        {

        }
    }
}

using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class User : RecordTimeStamps
    {
        [Key]
        public int UserId { get; set; }
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
    }
}

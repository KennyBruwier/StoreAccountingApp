using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public class PersonCredentials : Address
    {
        [Required]
        [MaxLength(20)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }
        public DateTime? BirthDay { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
    }
}

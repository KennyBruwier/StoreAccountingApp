using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class Client : PersonCredentials
    {
        [Key]
        public int ClientId { get; set; }
        [MaxLength(50)]
        public string Organisation { get; set; }
        [MaxLength(20)]
        public string CustomerCard { get; set; }
        [MaxLength(20)]
        public string Category { get; set; }
    }
}

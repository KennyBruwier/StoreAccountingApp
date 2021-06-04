using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class District : RecordTimeStamps
    {
        [Key]
        [MaxLength(20)]
        public string PostalCodeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}

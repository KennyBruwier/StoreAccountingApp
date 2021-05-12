using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Iso3166Code { get; set; }
        [MaxLength(20)]
        public string Capital { get; set; }
        [MaxLength(4)]
        public string PhoneCode { get; set; }
        public int TimeDiff_UTC { get; set; }
        public virtual ICollection<District> Districts { get; set;}
    }
}

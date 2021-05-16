using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public abstract class AddressDigitalShort : RecordTimeStamps
    {
        [Required]
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}

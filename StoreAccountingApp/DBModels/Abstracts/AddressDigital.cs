using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels.Abstracts
{
    public abstract class AddressDigital : AddressDigitalShort
    {
        [MaxLength(100)]
        public string Website { get; set; }
        [MaxLength(100)]
        public string LinkedIn { get; set; }
        [MaxLength(100)]
        public string Facebook { get; set; }
    }
}

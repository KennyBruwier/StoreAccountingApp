using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public abstract class Address : AddressDigital
    {
        [MaxLength(50)]
        public string Streetname { get; set; }
        [MaxLength(5)]
        public string HouseNr { get; set; }
        [MaxLength(5)]
        public string BoxNr { get; set; }
        [MaxLength(20)]
        public string PostalCodeId { get; set; }
        public virtual District District { get; set; }
        [MaxLength(20)]
        public string FaxNumber { get; set; }

    }
}

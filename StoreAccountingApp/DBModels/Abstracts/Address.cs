using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DBModels
{
    public abstract class Address : RecordTimeStamps
    {
        [MaxLength(50)]
        public string Streetname { get; set; }
        [MaxLength(5)]
        public string HouseNr { get; set; }
        [MaxLength(5)]
        public string BoxNr { get; set; }
        public string PostalCodeId { get; set; }
        public virtual District District { get; set; }
    }
}

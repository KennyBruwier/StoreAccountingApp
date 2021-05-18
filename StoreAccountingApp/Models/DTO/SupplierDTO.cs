using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.Models.DTO.Abstracts;

namespace StoreAccountingApp.Models.DTO
{
    public class SupplierDTO : PersonCredentialsDTO
    {
        private int supplierId;
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; OnPropertyChanged("SupplierId"); }
        }
        private string organisation;
        public string Organisation
        {
            get { return organisation; }
            set { organisation = value; OnPropertyChanged("Organisation"); }
        }
    }
}

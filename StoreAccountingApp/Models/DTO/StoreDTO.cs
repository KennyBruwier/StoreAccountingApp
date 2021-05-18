using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.Models.DTO.Abstracts;

namespace StoreAccountingApp.Models.DTO
{
    public class StoreDTO : ServiceBuildingCredentialsDTO
    {
        private int storeId;
        public int StoreId
        {
            get { return storeId; }
            set { storeId = value; OnPropertyChanged("StoreId"); }
        }
        private string dUNSNumber;
        public string DUNSNumber
        {
            get { return dUNSNumber; }
            set { dUNSNumber = value; OnPropertyChanged("DUNSNumber"); }
        }
    }
}

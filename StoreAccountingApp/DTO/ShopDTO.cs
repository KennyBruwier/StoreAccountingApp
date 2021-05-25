using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class ShopDTO : ServiceBuildingCredentialsDTO
    {
        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; OnPropertyChanged(nameof(ShopId)); }
        }
        private string dUNSNumber;
        public string DUNSNumber
        {
            get { return dUNSNumber; }
            set { dUNSNumber = value; OnPropertyChanged(nameof(DUNSNumber)); }
        }
    }
}

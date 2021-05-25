using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class OrderProductDTO:ItemTransactionDTO
    {
        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; OnPropertyChanged("OrderId"); }
        }
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; OnPropertyChanged("ProductId"); }
        }
    }
}

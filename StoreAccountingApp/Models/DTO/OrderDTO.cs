using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Models.DTO.Abstracts;

namespace StoreAccountingApp.Models.DTO
{
    public class OrderDTO : InvoiceDTO
    {
        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; OnPropertyChanged("OrderId"); }
        }
        private int supplierId;

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; OnPropertyChanged("SupplierId"); }
        }
        private int storeId;
        public int StoreId
        {
            get { return storeId; }
            set { storeId = value; OnPropertyChanged("StoreId"); }
        }
        private int employeeId;

        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; OnPropertyChanged("EmployeeId"); }
        }
    }
}

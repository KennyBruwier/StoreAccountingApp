using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.DTO.Abstracts;
using StoreAccountingApp.Models;

namespace StoreAccountingApp.DTO
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
        private string supplierName;

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; OnPropertyChanged("SupplierName"); }
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
        private EmployeeDTO employee = new EmployeeDTO();

        public EmployeeDTO Employee
        {
            get { return employee; }
            set { employee = value; OnPropertyChanged("Employee"); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(OrderId), OrderId);
            Validation.AddNonNullFields(nameof(SupplierId), SupplierId);
            Validation.AddNonNullFields(nameof(StoreId), StoreId);
            Validation.AddNonNullFields(nameof(EmployeeId), EmployeeId);
            Validation.AddNonNullFields(nameof(InvoiceNumber), InvoiceNumber);
            Validation.AddNonNullFields(nameof(Status), Status);
        }
    }
}

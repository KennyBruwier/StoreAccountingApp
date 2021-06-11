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
        private SupplierDTO supplierDTO;

        public SupplierDTO SupplierDTO
        {
            get { return supplierDTO; }
            set { supplierDTO = value; OnPropertyChanged(nameof(SupplierDTO)); }
        }
        private string supplierName;

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; OnPropertyChanged("SupplierName"); }
        }

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; OnPropertyChanged("ShopId"); }
        }
        private ShopDTO shopDTO;

        public ShopDTO ShopDTO
        {
            get { return shopDTO; }
            set { shopDTO = value; OnPropertyChanged(nameof(ShopDTO)); }
        }
        private string shopName;
        public string ShopName
        {
            get { return shopName; }
            set { shopName = value; OnPropertyChanged(nameof(ShopName)); }
        }

        private int employeeId;
        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; OnPropertyChanged("EmployeeId"); }
        }
        private EmployeeDTO employeeDTO;

        public EmployeeDTO EmployeeDTO
        {
            get { return employeeDTO; }
            set { employeeDTO = value; OnPropertyChanged(nameof(EmployeeDTO)); }
        }
        private string employeeFullname;

        public string EmployeeFullname
        {
            get { return employeeFullname; }
            set { employeeFullname = value; OnPropertyChanged(nameof(EmployeeFullname)); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(OrderId), OrderId);
            Validation.AddNonNullFields(nameof(SupplierId), SupplierId);
            Validation.AddNonNullFields(nameof(ShopId), ShopId);
            Validation.AddNonNullFields(nameof(EmployeeId), EmployeeId);
            Validation.AddNonNullFields(nameof(InvoiceNumber), InvoiceNumber);
            Validation.AddNonNullFields(nameof(Status), Status);
            Validation.AddUniqueValueFields(nameof(InvoiceNumber), InvoiceNumber);
        }
    }
}

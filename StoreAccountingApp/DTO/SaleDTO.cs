using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class SaleDTO : InvoiceDTO
    {
        private int saleId;
        public int SaleId
        {
            get { return saleId; }
            set { saleId = value; OnPropertyChanged("SaleId"); }
        }
        private int clientId;
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; OnPropertyChanged("ClientId"); }
        }
        private ClientDTO clientDTO;

        public ClientDTO ClientDTO
        {
            get { return clientDTO; }
            set { clientDTO = value; OnPropertyChanged(nameof(ClientDTO)); }
        }
        private string clientFullname;

        public string ClientFullname
        {
            get { return clientFullname; }
            set { clientFullname = value; OnPropertyChanged(nameof(ClientFullname)); }
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
            Validation.AddPrimaryKey(nameof(SaleId), SaleId);
            Validation.AddNonNullFields(nameof(ClientId), ClientId);
            Validation.AddNonNullFields(nameof(ShopId), ShopId);
            Validation.AddNonNullFields(nameof(EmployeeId), EmployeeId);
            Validation.AddNonNullFields(nameof(InvoiceNumber), InvoiceNumber);
            Validation.AddNonNullFields(nameof(Status), Status);
            Validation.AddUniqueValueFields(nameof(InvoiceNumber), InvoiceNumber);
        }
    }
}

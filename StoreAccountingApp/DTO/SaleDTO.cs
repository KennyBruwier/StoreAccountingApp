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
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(SaleId), SaleId);
            Validation.AddNonNullFields(nameof(ClientId), ClientId);
            Validation.AddNonNullFields(nameof(StoreId), StoreId);
            Validation.AddNonNullFields(nameof(EmployeeId), EmployeeId);
            Validation.AddNonNullFields(nameof(InvoiceNumber), InvoiceNumber);
            Validation.AddNonNullFields(nameof(Status), Status);
        }
    }
}

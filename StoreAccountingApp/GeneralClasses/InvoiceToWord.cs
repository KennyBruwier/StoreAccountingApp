using StoreAccountingApp.DTO.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.GeneralClasses
{
    public class InvoiceToWord<DTOModel> where DTOModel : InvoiceDTO
    {
        private DTOModel currentInvoice;
        public DTOModel CurrentInvoice
        {
            get { return currentInvoice; }
            set { currentInvoice = value; }
        }
        private List<OrdersDataGrid> productsList;
        public List<OrdersDataGrid> ProductsList
        {
            get { return productsList; }
            set 
            { 
                productsList = value;  
            }
        }
        private string companyName = "Company Name";

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }
        private string footer = "Thank you for your business";

        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }
        public void AddProductList(List<OrdersDataGrid>listToAdd)
        {
            List<OrdersDataGrid> newList = new List<OrdersDataGrid>();
            OrdersDataGrid totalRow = new OrdersDataGrid()
            {
                Key = "Total",
                UnitPrice = 0
            };
            foreach (OrdersDataGrid item in listToAdd)
            {
                totalRow.Amount += item.Amount;
                totalRow.Netto += item.Netto;
                totalRow.VAT += item.VAT;
                totalRow.Total += item.Total;
                newList.Add(item);
            }
            newList.Add(totalRow);
            //newList.Add(new OrdersDataGrid()
            //{
            //    Key = "Total",
            //    UnitPrice = 0,
            //    Amount = totalRow.Amount,
            //    Netto = totalRow.Netto,
            //    VAT = totalRow.VAT,
            //    Total = totalRow.Total
            //});
            
            productsList = newList;
        }

    }
}

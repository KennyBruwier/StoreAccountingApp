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
        private OrderDTO orderDTO;

        public OrderDTO OrderDTO
        {
            get { return orderDTO; }
            set { orderDTO = value; OnPropertyChanged(nameof(OrderDTO)); }
        }

        private string orderInvoiceNr;

        public string OrderInvoiceNr
        {
            get { return orderInvoiceNr; }
            set { orderInvoiceNr = value; OnPropertyChanged("OrderInvoiceNr"); }
        }

        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; OnPropertyChanged("ProductId"); }
        }
        private ProductDTO productDTO;

        public ProductDTO ProductDTO
        {
            get { return productDTO; }
            set { productDTO = value; OnPropertyChanged(nameof(ProductDTO)); }
        }
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; OnPropertyChanged("ProductName"); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(OrderId), OrderId);
            Validation.AddPrimaryKey(nameof(ProductId), ProductId);
            Validation.AddNonNullFields(nameof(Status), Status);
        }
    }
}

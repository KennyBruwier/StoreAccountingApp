using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DTO;
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels.Orders
{
    public class OrdersOrderViewModel : ViewModelBase
    {
        private ProductService _productService;
        private OrderService _orderService;
        private OrderProductService _orderProductService;
        private List<ProductDTO> productsList;

        public List<ProductDTO> ProductsList
        {
            get { return productsList; }
            set { productsList = value; OnPropertyChanged(nameof(ProductsList)); }
        }
        private List<OrderDTO> ordersList;

        public List<OrderDTO> OrdersList
        {
            get { return ordersList; }
            set { ordersList = value; OnPropertyChanged(nameof(OrdersList)); }
        }
        private List<OrderProductDTO> orderProductList;

        public List<OrderProductDTO> OrderProductList
        {
            get { return orderProductList; }
            set { orderProductList = value; OnPropertyChanged(nameof(OrderProductList)); }
        }
        private List<ComboboxItem> cbOrdersList;

        public List<ComboboxItem> CbOrdersList
        {
            get { return cbOrdersList; }
            set { cbOrdersList = value; OnPropertyChanged(nameof(CbOrdersList)); }
        }
        private List<OrdersDataGrid> dgList;

        public List<OrdersDataGrid> DgList
        {
            get { return dgList; }
            set { dgList = value; OnPropertyChanged(nameof(DgList)); }
        }
        private OrderDTO currentOrder;

        public OrderDTO CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; OnPropertyChanged(nameof(CurrentOrder)); }
        }

        public OrdersOrderViewModel()
        {
            _productService = new ProductService();
            _orderProductService = new OrderProductService();
            _orderService = new OrderService();
            OrderProductList = _orderProductService.GetAll();
            ProductsList = _productService.GetAll().Where(p => OrderProductList.Select(op => op.ProductId).Contains(p.ProductId)).ToList(); ;
            OrdersList = _orderService.GetAll();
            CbOrdersList = ObjMethods.CreateComboboxList<OrderDTO, ComboboxItem>(OrdersList, "OderId", "InvoiceNumber");
            DgList = CreateDataGridListFromOrderProducts();
        }
        public List<OrdersDataGrid>CreateDataGridListFromOrderProducts()
        {
            OrdersDataGrid grandTotal = null;
            List<OrdersDataGrid> newDataGridList = new List<OrdersDataGrid>();
            var dDgList = OrderProductList.Join(ProductsList,
                op => op.ProductId,
                p => p.ProductId,
                (op, p) => new
                {
                    op,
                    p
                })
                .GroupBy(p => p.p.Name)
                .Select(g => new
                {
                    Key = g.Key,
                    Count = g.Sum(s => s.op.Amount),
                    Net = g.Sum(s => s.op.UnitPrice),
                    Vat = g.Sum(s => s.op.VAT),
                    Total = g.Sum(s => (s.op.UnitPrice + s.op.VAT))
                })
                .OrderByDescending(g => g.Total).ToList();
            foreach (var item in dDgList)
            {
                OrdersDataGrid dgItem = new OrdersDataGrid(item);
                if (grandTotal == null) grandTotal = new OrdersDataGrid() { Key = "Total: " };
                grandTotal.Total += dgItem.Total;
                grandTotal.Netto += dgItem.Netto;
                grandTotal.VAT += dgItem.VAT;
                grandTotal.Amount += dgItem.Amount;
                newDataGridList.Add(dgItem);
            }
            if (grandTotal != null)
                newDataGridList.Add(grandTotal);
            return newDataGridList;
        }

    }
}

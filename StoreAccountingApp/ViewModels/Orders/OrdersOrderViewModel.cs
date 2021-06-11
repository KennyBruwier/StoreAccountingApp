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
    public class OrdersOrderViewModel : BaseOrderViewModel<OrderDTO>
    {
        private ProductService _productService;
        private OrderService _orderService;
        private OrderProductService _orderProductService;
        private SupplierService _supplierService;
        private List<SupplierDTO> supplierList;

        public List<SupplierDTO> SupplierList
        {
            get { return supplierList; }
            set { supplierList = value; OnPropertyChanged(nameof(SupplierList)); }
        }

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
        private ComboboxItem selectedOrder;

        public ComboboxItem SelectedOrder
        {
            get { return selectedOrder; }
            set 
            { 
                selectedOrder = value;
                CurrentOrder = OrdersList.Where(o => o.OrderId == value.Key).FirstOrDefault();
                DgList = CreateDataGridList();
                if (InvoiceToPrint == null) InvoiceToPrint = new InvoiceToWord<OrderDTO>();
                InvoiceToPrint.AddProductList(DgList);
                InvoiceToPrint.CurrentInvoice = CurrentOrder;
                OnPropertyChanged(nameof(SelectedOrder)); 
            }
        }
        public event Action CurrentDataGridListChanged;

        public OrdersOrderViewModel()
        {
            _productService = new ProductService();
            _orderProductService = new OrderProductService();
            _orderService = new OrderService();
            OrderProductList = _orderProductService.GetAll();
            ProductsList = _productService.GetAll().Where(p => OrderProductList.Select(op => op.ProductId).Contains(p.ProductId)).ToList(); ;
            OrdersList = _orderService.GetAll();
            CbOrdersList = ObjMethods.CreateComboboxList<OrderDTO, ComboboxItem>(OrdersList, "OrderId", "InvoiceNumber");
            DgList = CreateDataGridList();
            this.CurrentDataGridListChanged += OnCurrentDataGridListChange;
        }
        private void OnCurrentDataGridListChange()
        {
            OnPropertyChanged(nameof(DgList));
        }
        public List<OrdersDataGrid> CreateDataGridList()
        {
            OrdersDataGrid grandTotal = null;
            List<OrdersDataGrid> newDataGridList = new List<OrdersDataGrid>();
            if (currentOrder != null && currentOrder.OrderId != 0)
            {
                var dDgList = OrderProductList.Where(sp => sp.OrderId == currentOrder.OrderId).Join(ProductsList,
                    op => op.ProductId,
                    p => p.ProductId,
                    (op, p) => new
                    {
                        op,
                        p
                    })
                    .GroupBy(p => new { Key = p.p.Name, UnitPrice = p.op.UnitPrice })
                    .Select(g => new
                    {
                        Key = g.Key.Key,
                        UnitPrice = g.Key.UnitPrice,
                        Amount = g.Sum(s => s.op.Amount),
                        Netto = g.Key.UnitPrice * g.Sum(s => s.op.Amount),
                        VAT = g.Sum(s => (s.op.VAT * s.op.Amount)),
                        Total = g.Key.UnitPrice * g.Sum(s => s.op.Amount) + g.Sum(s => (s.op.VAT * s.op.Amount))
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

            }
            else
            {
                var dDgList = OrderProductList.Join(ProductsList,
                    op => op.ProductId,
                    p => p.ProductId,
                    (op, p) => new
                    {
                        op,
                        p
                    })
                    .GroupBy(p => new { Key = p.p.Name, UnitPrice = p.op.UnitPrice })
                    .Select(g => new
                    {
                        Key = g.Key.Key,
                        UnitPrice = g.Key.UnitPrice,
                        Amount = g.Sum(s => s.op.Amount),
                        Netto = g.Key.UnitPrice * g.Sum(s => s.op.Amount),
                        VAT = g.Sum(s => (s.op.VAT * s.op.Amount)),
                        Total = g.Key.UnitPrice * g.Sum(s => s.op.Amount) + g.Sum(s => (s.op.VAT * s.op.Amount))
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
            }

            return newDataGridList;

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

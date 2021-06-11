using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Services;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using StoreAccountingApp.CustomMethods;
using System.Windows.Input;
using System.Reflection;
using System.Data.Entity.Validation;
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Models;
using StoreAccountingApp.Stores;

namespace StoreAccountingApp.ViewModels
{
    public class DBOrderProductViewModel : DBViewModelBase<OrderProductDTO,OrderProductService,OrderProduct>
    {
        private readonly OrderService _OrderService;
        private readonly ProductService _ProductService;
        public DBOrderProductViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
            _ProductService = new ProductService();
            _OrderService = new OrderService();
            LoadData();
        }
        #region DisplayOperation
        private List<ProductDTO> productList;
        public List<ProductDTO> ProductList
        {
            get { return productList; }
            set { productList = value; OnPropertyChanged("ProductList"); }
        }
        private List<ComboboxItem> cbProductList;
        public List<ComboboxItem> CbProductList
        {
            get { return cbProductList; }
            set { cbProductList = value; OnPropertyChanged("CbProductList"); }
        }
        private List<OrderDTO> orderList;
        public List<OrderDTO> OrderList
        {
            get { return orderList; }
            set { orderList = value; OnPropertyChanged("OrderList"); }
        }
        private List<ComboboxItem> cbOrderList;
        public List<ComboboxItem> CbOrderList
        {
            get { return cbOrderList; }
            set { cbOrderList = value; OnPropertyChanged("CbOrderList"); }
        }
        private ComboboxItem selectedOrder;

        public ComboboxItem SelectedOrder
        {
            get { return selectedOrder; }
            set 
            { 
                selectedOrder = value; 
                OnPropertyChanged(nameof(SelectedOrder));
                CurrentDTOModel.OrderId = selectedOrder.Key;
            }
        }
        private ComboboxItem selectedProduct;

        public ComboboxItem SelectedProduct
        {
            get { return selectedProduct; }
            set 
            { 
                selectedProduct = value; 
                OnPropertyChanged(nameof(SelectedOrder));
                CurrentDTOModel.ProductId = selectedProduct.Key;
            }
        }

        #endregion
        private void LoadData()
        {
            ProductList = _ProductService.GetAll();
            OrderList = _OrderService.GetAll();
            cbProductList = ObjMethods.CreateComboboxList<ProductDTO, ComboboxItem>(ProductList, "ProductId", "Name");
            cbOrderList = ObjMethods.CreateComboboxList<OrderDTO, ComboboxItem>(OrderList, "OrderId", "InvoiceNumber");
        }
    }
}

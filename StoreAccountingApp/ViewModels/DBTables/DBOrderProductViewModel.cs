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

namespace StoreAccountingApp.ViewModels
{
    public class DBOrderProductViewModel : DBViewModelBase
    {
        private OrderProductService _OrderProductService;
        private OrderService _OrderService;
        private ProductService _ProductService;
        private OrderProductDTO currentOrderProductDTO;
        public OrderProductDTO CurrentOrderProductDTO
        {
            get { return currentOrderProductDTO; }
            set { currentOrderProductDTO = value; OnPropertyChanged("CurrentOrderProductDTO"); }
        }
        public DBOrderProductViewModel()
        {
            _OrderProductService = new OrderProductService();
            _ProductService = new ProductService();
            _OrderService = new OrderService();
            LoadData();
            CurrentOrderProductDTO = new OrderProductDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<OrderProductDTO> orderProductList;
        public List<OrderProductDTO> OrderProductList
        {
            get { return orderProductList; }
            set { orderProductList = value; OnPropertyChanged("OrderProductList"); }
        }
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

        private void LoadData()
        {
            OrderProductList = _OrderProductService.GetAll();
            ProductList = _ProductService.GetAll();
            OrderList = _OrderService.GetAll();
            cbProductList = ObjMethods.CreateComboboxList<ProductDTO, ComboboxItem>(ProductList, "ProductId", "Name", "Manufacturer");
            cbOrderList = ObjMethods.CreateComboboxList<OrderDTO, ComboboxItem>(OrderList, "OrderId", "InvoiceNumber", "SupplierName");
        }
        
        #endregion
        #region SaveOperation
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        public void Save()
        {
            try
            {
                var IsSaved = _OrderProductService.Add(CurrentOrderProductDTO);
                LoadData();
                if (IsSaved)
                    Message = "OrderProduct saved";
                else
                    Message = "Save operation failed";
            }
            catch (DbEntityValidationException ex)
            {
                Message = CreateValidationErrorMsg(ex);
            }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }
        #endregion
        #region SearchOperation
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        public void Search()
        {
            try
            {
                var ObjOrderProduct = _OrderProductService.Search(CurrentOrderProductDTO.OrderId, currentOrderProductDTO.ProductId);
                if (ObjOrderProduct != null)
                {
                    CurrentOrderProductDTO = ObjOrderProduct;
                    Message = "OrderProduct found";
                }
                else
                {
                    CurrentOrderProductDTO = new OrderProductDTO(); // empty the textbox fields
                    Message = "OrderProduct not found";
                }
            }
            catch (DbEntityValidationException ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
        #region UpdateOperation
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                if (_OrderProductService.Update(CurrentOrderProductDTO))
                {
                    Message = "OrderProduct updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (DbEntityValidationException ex)
            {
                Message = CreateValidationErrorMsg(ex);
            }
        }
        #endregion
        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                if (_OrderProductService.Delete(CurrentOrderProductDTO.OrderId, currentOrderProductDTO.ProductId))
                {
                    Message = "OrderProduct Deleted";
                    LoadData();
                }
                else
                    Message = "Delete operation failed";
            }
            catch (DbEntityValidationException ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}

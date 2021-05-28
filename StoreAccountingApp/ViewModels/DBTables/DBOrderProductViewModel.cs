using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Services;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class DBOrderProductViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        OrderProductService ObjOrderProductService;
        private OrderProductDTO currentOrderProductDTO;
        public OrderProductDTO CurrentOrderProductDTO
        {
            get { return currentOrderProductDTO; }
            set { currentOrderProductDTO = value; OnPropertyChanged("CurrentOrderProductDTO"); }
        }
        public DBOrderProductViewModel(INavigationService<HomeViewModel> homeNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            ObjOrderProductService = new OrderProductService();
            LoadData();
            CurrentOrderProductDTO = new OrderProductDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<OrderProductDTO> accountTypeList;
        public List<OrderProductDTO> OrderProductList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("OrderProductList"); }
        }
        private void LoadData()
        {
            OrderProductList = ObjOrderProductService.GetAll();
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
                var IsSaved = ObjOrderProductService.Add(CurrentOrderProductDTO);
                LoadData();
                if (IsSaved)
                    Message = "OrderProduct saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
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
                var ObjOrderProduct = ObjOrderProductService.Search(CurrentOrderProductDTO.OrderId, currentOrderProductDTO.ProductId);
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
            catch (Exception ex)
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
                if (ObjOrderProductService.Update(CurrentOrderProductDTO))
                {
                    Message = "OrderProduct updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
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
                if (ObjOrderProductService.Delete(CurrentOrderProductDTO.OrderId, currentOrderProductDTO.ProductId))
                {
                    Message = "OrderProduct Deleted";
                    LoadData();
                }
                else
                    Message = "Delete operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}

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
    public class DBOrderViewModel : DBViewModelBase
    {
        OrderService _orderService;
        private OrderDTO currentOrderDTO;
        public OrderDTO CurrentOrderDTO
        {
            get { return currentOrderDTO; }
            set { currentOrderDTO = value; OnPropertyChanged("CurrentOrderDTO"); }
        }
        public DBOrderViewModel()
        {
            _orderService = new OrderService();
            LoadData();
            CurrentOrderDTO = new OrderDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<OrderDTO> orderList;
        public List<OrderDTO> OrderList
        {
            get { return orderList; }
            set { orderList = value; OnPropertyChanged("OrderList"); }
        }
        private void LoadData()
        {
            OrderList = _orderService.GetAll();
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
                var IsSaved = _orderService.Add(CurrentOrderDTO);
                LoadData();
                if (IsSaved)
                    Message = "Order saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
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
                var order = _orderService.Search(CurrentOrderDTO.OrderId);
                if (order != null)
                {
                    CurrentOrderDTO = order;
                    Message = "Order found";
                }
                else
                {
                    CurrentOrderDTO = new OrderDTO(); // empty the textbox fields
                    Message = "Order not found";
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
                if (_orderService.Update(CurrentOrderDTO))
                {
                    Message = "Order updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
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
                if (_orderService.Delete(CurrentOrderDTO.OrderId))
                {
                    Message = "Order Deleted";
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

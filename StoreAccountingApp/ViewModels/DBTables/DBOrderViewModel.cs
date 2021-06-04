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
using System.Data.Entity.Validation;
using StoreAccountingApp.GeneralClasses;

namespace StoreAccountingApp.ViewModels
{
    public class DBOrderViewModel : ViewModelBase
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
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return _orderService.Add(CurrentOrderDTO);
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
        public void UpdateAndCatch()
        {
            CatchOperation(Update);
            LoadData();
        }
        public bool Update()
        {
            return _orderService.Update(CurrentOrderDTO);
        }
        #endregion
        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void DeleteAndCatch()
        {
            CatchOperation(Delete);
            LoadData();
        }
        public bool Delete()
        {
            return _orderService.Delete(CurrentOrderDTO.OrderId);
        }
        #endregion
    }
}

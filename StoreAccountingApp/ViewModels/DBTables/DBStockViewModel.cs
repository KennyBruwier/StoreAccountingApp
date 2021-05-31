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
    public class DBStockViewModel : ViewModelBase
    {
        StockService ObjStockService;
        private StockDTO currentStockDTO;
        public StockDTO CurrentStockDTO
        {
            get { return currentStockDTO; }
            set { currentStockDTO = value; OnPropertyChanged("CurrentStockDTO"); }
        }
        public DBStockViewModel()
        {
            ObjStockService = new StockService();
            LoadData();
            CurrentStockDTO = new StockDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<StockDTO> accountTypeList;
        public List<StockDTO> StockList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("StockList"); }
        }
        private void LoadData()
        {
            StockList = ObjStockService.GetAll();
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
                var IsSaved = ObjStockService.Add(CurrentStockDTO);
                LoadData();
                if (IsSaved)
                    Message = "Stock saved";
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
                var ObjStock = ObjStockService.Search(CurrentStockDTO.StoreId,CurrentStockDTO.ProductId);
                if (ObjStock != null)
                {
                    CurrentStockDTO = ObjStock;
                    Message = "Stock found";
                }
                else
                {
                    CurrentStockDTO = new StockDTO(); // empty the textbox fields
                    Message = "Stock not found";
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
                if (ObjStockService.Update(CurrentStockDTO))
                {
                    Message = "Stock updated";
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
                if (ObjStockService.Delete(CurrentStockDTO.StoreId, CurrentStockDTO.ProductId))
                {
                    Message = "Stock Deleted";
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

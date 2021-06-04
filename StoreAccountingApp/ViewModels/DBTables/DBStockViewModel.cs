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
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return ObjStockService.Add(CurrentStockDTO);
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
            return ObjStockService.Update(CurrentStockDTO);
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
            return ObjStockService.Delete(CurrentStockDTO.StoreId, CurrentStockDTO.ProductId);
        }
        #endregion
    }
}

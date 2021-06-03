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

namespace StoreAccountingApp.ViewModels
{
    public class DBSaleViewModel : DBViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        SaleService ObjSaleService;
        private SaleDTO currentSaleDTO;
        public SaleDTO CurrentSaleDTO
        {
            get { return currentSaleDTO; }
            set { currentSaleDTO = value; OnPropertyChanged("CurrentSaleDTO"); }
        }
        public DBSaleViewModel()
        {
            ObjSaleService = new SaleService();
            LoadData();
            CurrentSaleDTO = new SaleDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<SaleDTO> accountTypeList;
        public List<SaleDTO> SaleList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("SaleList"); }
        }
        private void LoadData()
        {
            SaleList = ObjSaleService.GetAll();
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
                var IsSaved = ObjSaleService.Add(CurrentSaleDTO);
                LoadData();
                if (IsSaved)
                    Message = "Sale saved";
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
                var ObjSale = ObjSaleService.Search(CurrentSaleDTO.SaleId);
                if (ObjSale != null)
                {
                    CurrentSaleDTO = ObjSale;
                    Message = "Sale found";
                }
                else
                {
                    CurrentSaleDTO = new SaleDTO(); // empty the textbox fields
                    Message = "Sale not found";
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
                if (ObjSaleService.Update(CurrentSaleDTO))
                {
                    Message = "Sale updated";
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
                if (ObjSaleService.Delete(CurrentSaleDTO.SaleId))
                {
                    Message = "Sale Deleted";
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

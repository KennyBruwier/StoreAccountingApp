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
    public class DBSaleViewModel : ViewModelBase
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
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return ObjSaleService.Add(CurrentSaleDTO);
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
        public void UpdateAndCatch()
        {
            CatchOperation(Update);
            LoadData();
        }
        public bool Update()
        {
            return ObjSaleService.Update(CurrentSaleDTO);
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
            return ObjSaleService.Delete(CurrentSaleDTO.SaleId);
        }
        #endregion
    }
}

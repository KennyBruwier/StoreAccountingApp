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
    public class DBSaleProductViewModel : ViewModelBase
    {
        SaleProductService ObjSaleProductService;
        private SaleProductDTO currentSaleProductDTO;
        public SaleProductDTO CurrentSaleProductDTO
        {
            get { return currentSaleProductDTO; }
            set { currentSaleProductDTO = value; OnPropertyChanged("CurrentSaleProductDTO"); }
        }
        public DBSaleProductViewModel()
        {
            ObjSaleProductService = new SaleProductService();
            LoadData();
            CurrentSaleProductDTO = new SaleProductDTO();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
        }
        #region DisplayOperation
        private List<SaleProductDTO> accountTypeList;
        public List<SaleProductDTO> SaleProductList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("SaleProductList"); }
        }
        private void LoadData()
        {
            SaleProductList = ObjSaleProductService.GetAll();
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
            return ObjSaleProductService.Add(CurrentSaleProductDTO);
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
                var ObjSaleProduct = ObjSaleProductService.Search(CurrentSaleProductDTO.SaleId,CurrentSaleProductDTO.ProductId);
                if (ObjSaleProduct != null)
                {
                    CurrentSaleProductDTO = ObjSaleProduct;
                    Message = "SaleProduct found";
                }
                else
                {
                    CurrentSaleProductDTO = new SaleProductDTO(); // empty the textbox fields
                    Message = "SaleProduct not found";
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
            return ObjSaleProductService.Update(CurrentSaleProductDTO);
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
            return ObjSaleProductService.Delete(CurrentSaleProductDTO.SaleId, CurrentSaleProductDTO.ProductId);
        }
        #endregion
    }
}

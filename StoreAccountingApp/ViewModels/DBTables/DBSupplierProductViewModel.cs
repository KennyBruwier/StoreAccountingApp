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
    public class DBSupplierProductViewModel : ViewModelBase
    {
        SupplierProductService ObjSupplierProductService;
        private SupplierProductDTO currentSupplierProductDTO;
        public SupplierProductDTO CurrentSupplierProductDTO
        {
            get { return currentSupplierProductDTO; }
            set { currentSupplierProductDTO = value; OnPropertyChanged("CurrentSupplierProductDTO"); }
        }
        public DBSupplierProductViewModel()
        {
            ObjSupplierProductService = new SupplierProductService();
            LoadData();
            CurrentSupplierProductDTO = new SupplierProductDTO();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
        }
        #region DisplayOperation
        private List<SupplierProductDTO> accountTypeList;
        public List<SupplierProductDTO> SupplierProductList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("SupplierProductList"); }
        }
        private void LoadData()
        {
            SupplierProductList = ObjSupplierProductService.GetAll();
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
            return ObjSupplierProductService.Add(CurrentSupplierProductDTO);
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
                var ObjSupplierProduct = ObjSupplierProductService.Search(CurrentSupplierProductDTO.SupplierId, CurrentSupplierProductDTO.ProductId);
                if (ObjSupplierProduct != null)
                {
                    CurrentSupplierProductDTO = ObjSupplierProduct;
                    Message = "SupplierProduct found";
                }
                else
                {
                    CurrentSupplierProductDTO = new SupplierProductDTO(); // empty the textbox fields
                    Message = "SupplierProduct not found";
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
            return ObjSupplierProductService.Update(CurrentSupplierProductDTO);
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
            return ObjSupplierProductService.Delete(CurrentSupplierProductDTO.SupplierId, CurrentSupplierProductDTO.ProductId);
        }
        #endregion
    }
}

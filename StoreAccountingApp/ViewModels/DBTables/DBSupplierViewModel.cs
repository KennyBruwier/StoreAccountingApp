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
    public class DBSupplierViewModel : ViewModelBase
    {
        SupplierService ObjSupplierService;
        private SupplierDTO currentSupplierDTO;
        public SupplierDTO CurrentSupplierDTO
        {
            get { return currentSupplierDTO; }
            set { currentSupplierDTO = value; OnPropertyChanged("CurrentSupplierDTO"); }
        }
        public DBSupplierViewModel()
        {
            ObjSupplierService = new SupplierService();
            LoadData();
            CurrentSupplierDTO = new SupplierDTO();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
        }
        #region DisplayOperation
        private List<SupplierDTO> accountTypeList;
        public List<SupplierDTO> SupplierList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("SupplierList"); }
        }
        private void LoadData()
        {
            SupplierList = ObjSupplierService.GetAll();
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
            return ObjSupplierService.Add(CurrentSupplierDTO);
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
                var ObjSupplier = ObjSupplierService.Search(CurrentSupplierDTO.SupplierId);
                if (ObjSupplier != null)
                {
                    CurrentSupplierDTO = ObjSupplier;
                    Message = "Supplier found";
                }
                else
                {
                    CurrentSupplierDTO = new SupplierDTO(); // empty the textbox fields
                    Message = "Supplier not found";
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
            return ObjSupplierService.Update(CurrentSupplierDTO);
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
            return ObjSupplierService.Delete(CurrentSupplierDTO.SupplierId);
        }
        #endregion
    }
}

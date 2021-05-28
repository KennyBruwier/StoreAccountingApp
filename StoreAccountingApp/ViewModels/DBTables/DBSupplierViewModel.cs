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
    public class DBSupplierViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        SupplierService ObjSupplierService;
        private SupplierDTO currentSupplierDTO;
        public SupplierDTO CurrentSupplierDTO
        {
            get { return currentSupplierDTO; }
            set { currentSupplierDTO = value; OnPropertyChanged("CurrentSupplierDTO"); }
        }
        public DBSupplierViewModel(INavigationService<HomeViewModel> homeNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            ObjSupplierService = new SupplierService();
            LoadData();
            CurrentSupplierDTO = new SupplierDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
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
        public void Save()
        {
            try
            {
                var IsSaved = ObjSupplierService.Add(CurrentSupplierDTO);
                LoadData();
                if (IsSaved)
                    Message = "Supplier saved";
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
                if (ObjSupplierService.Update(CurrentSupplierDTO))
                {
                    Message = "Supplier updated";
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
                if (ObjSupplierService.Delete(CurrentSupplierDTO.SupplierId))
                {
                    Message = "Supplier Deleted";
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

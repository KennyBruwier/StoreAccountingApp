using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Services;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;

namespace StoreAccountingApp.ViewModels
{
    public class SupplierProductViewModel : ViewModelBase
    {
        SupplierProductService ObjSupplierProductService;
        private SupplierProductDTO currentSupplierProductDTO;
        public SupplierProductDTO CurrentSupplierProductDTO
        {
            get { return currentSupplierProductDTO; }
            set { currentSupplierProductDTO = value; OnPropertyChanged("CurrentSupplierProductDTO"); }
        }
        public SupplierProductViewModel()
        {
            ObjSupplierProductService = new SupplierProductService();
            LoadData();
            CurrentSupplierProductDTO = new SupplierProductDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
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
        public void Save()
        {
            try
            {
                var IsSaved = ObjSupplierProductService.Add(CurrentSupplierProductDTO);
                LoadData();
                if (IsSaved)
                    Message = "SupplierProduct saved";
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
                if (ObjSupplierProductService.Update(CurrentSupplierProductDTO))
                {
                    Message = "SupplierProduct updated";
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
                if (ObjSupplierProductService.Delete(CurrentSupplierProductDTO.SupplierId, CurrentSupplierProductDTO.ProductId))
                {
                    Message = "SupplierProduct Deleted";
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

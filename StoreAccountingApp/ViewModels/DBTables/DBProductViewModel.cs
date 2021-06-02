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
    public class DBProductViewModel : DBViewModelBase
    {
        ProductService ObjProductService;
        private ProductDTO currentProductDTO;
        public ProductDTO CurrentProductDTO
        {
            get { return currentProductDTO; }
            set { currentProductDTO = value; OnPropertyChanged("CurrentProductDTO"); }
        }
        public DBProductViewModel()
        {
            ObjProductService = new ProductService();
            LoadData();
            CurrentProductDTO = new ProductDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<ProductDTO> accountTypeList;
        public List<ProductDTO> ProductList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("ProductList"); }
        }
        private void LoadData()
        {
            ProductList = ObjProductService.GetAll();
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
                var IsSaved = ObjProductService.Add(CurrentProductDTO);
                LoadData();
                if (IsSaved)
                    Message = "Product saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
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
                var ObjProduct = ObjProductService.Search(CurrentProductDTO.ProductId);
                if (ObjProduct != null)
                {
                    CurrentProductDTO = ObjProduct;
                    Message = "Product found";
                }
                else
                {
                    CurrentProductDTO = new ProductDTO(); // empty the textbox fields
                    Message = "Product not found";
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
                if (ObjProductService.Update(CurrentProductDTO))
                {
                    Message = "Product updated";
                    LoadData();
                }
                else
                {
                    Message = "Product failed to update";
                }
            }
            catch (Exception ex)
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
                if (ObjProductService.Delete(CurrentProductDTO.ProductId))
                {
                    Message = "Product Deleted";
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

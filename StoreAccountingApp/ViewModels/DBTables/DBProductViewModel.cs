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
    public class DBProductViewModel : ViewModelBase
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
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return ObjProductService.Add(CurrentProductDTO);
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
            return ObjProductService.Update(CurrentProductDTO);
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
            return ObjProductService.Delete(CurrentProductDTO.ProductId);
        }
        #endregion
    }
}

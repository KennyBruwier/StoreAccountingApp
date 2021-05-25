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
    public class SaleProductViewModel : ViewModelBase
    {
        SaleProductService ObjSaleProductService;
        private SaleProductDTO currentSaleProductDTO;
        public SaleProductDTO CurrentSaleProductDTO
        {
            get { return currentSaleProductDTO; }
            set { currentSaleProductDTO = value; OnPropertyChanged("CurrentSaleProductDTO"); }
        }
        public SaleProductViewModel()
        {
            ObjSaleProductService = new SaleProductService();
            LoadData();
            CurrentSaleProductDTO = new SaleProductDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
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
        public void Save()
        {
            try
            {
                var IsSaved = ObjSaleProductService.Add(CurrentSaleProductDTO);
                LoadData();
                if (IsSaved)
                    Message = "SaleProduct saved";
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
                if (ObjSaleProductService.Update(CurrentSaleProductDTO))
                {
                    Message = "SaleProduct updated";
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
                if (ObjSaleProductService.Delete(CurrentSaleProductDTO.SaleId, CurrentSaleProductDTO.ProductId))
                {
                    Message = "SaleProduct Deleted";
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

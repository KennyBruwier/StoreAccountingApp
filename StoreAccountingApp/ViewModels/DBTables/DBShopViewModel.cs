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
    public class DBShopViewModel : ViewModelBase
    {
        ShopService ObjShopService;
        private ShopDTO currentShopDTO;
        public ShopDTO CurrentShopDTO
        {
            get { return currentShopDTO; }
            set { currentShopDTO = value; OnPropertyChanged("CurrentShopDTO"); }
        }
        public DBShopViewModel()
        {
            ObjShopService = new ShopService();
            LoadData();
            CurrentShopDTO = new ShopDTO();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
        }
        #region DisplayOperation
        private List<ShopDTO> accountTypeList;
        public List<ShopDTO> ShopList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("ShopList"); }
        }
        private void LoadData()
        {
            ShopList = ObjShopService.GetAll();
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
            return ObjShopService.Add(CurrentShopDTO);
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
                var ObjShop = ObjShopService.Search(CurrentShopDTO.ShopId);
                if (ObjShop != null)
                {
                    CurrentShopDTO = ObjShop;
                    Message = "Shop found";
                }
                else
                {
                    CurrentShopDTO = new ShopDTO(); // empty the textbox fields
                    Message = "Shop not found";
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
            return ObjShopService.Update(CurrentShopDTO);
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
            return ObjShopService.Delete(CurrentShopDTO.ShopId);
        }
        #endregion
    }
}

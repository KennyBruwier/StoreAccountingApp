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
    public class DBShopViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        ShopService ObjShopService;
        private ShopDTO currentShopDTO;
        public ShopDTO CurrentShopDTO
        {
            get { return currentShopDTO; }
            set { currentShopDTO = value; OnPropertyChanged("CurrentShopDTO"); }
        }
        public DBShopViewModel(INavigationService<HomeViewModel> homeNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            ObjShopService = new ShopService();
            LoadData();
            CurrentShopDTO = new ShopDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
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
        public void Save()
        {
            try
            {
                var IsSaved = ObjShopService.Add(CurrentShopDTO);
                LoadData();
                if (IsSaved)
                    Message = "Shop saved";
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
                if (ObjShopService.Update(CurrentShopDTO))
                {
                    Message = "Shop updated";
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
                if (ObjShopService.Delete(CurrentShopDTO.ShopId))
                {
                    Message = "Store Deleted";
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

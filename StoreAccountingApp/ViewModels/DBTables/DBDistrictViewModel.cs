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
    public class DBDistrictViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        DistrictService ObjDistrictService;
        private DistrictDTO currentDistrictDTO;
        public DistrictDTO CurrentDistrictDTO
        {
            get { return currentDistrictDTO; }
            set { currentDistrictDTO = value; OnPropertyChanged("CurrentDistrictDTO"); }
        }
        public DBDistrictViewModel(INavigationService<HomeViewModel> homeNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            ObjDistrictService = new DistrictService();
            LoadData();
            CurrentDistrictDTO = new DistrictDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<DistrictDTO>accountTypeList;
        public List<DistrictDTO>DistrictList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("DistrictList"); }
        }
        private void LoadData()
        {
            DistrictList = ObjDistrictService.GetAll();
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
                var IsSaved = ObjDistrictService.Add(CurrentDistrictDTO);
                LoadData();
                if (IsSaved)
                    Message = "District saved";
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
                var ObjDistrict = ObjDistrictService.Search(CurrentDistrictDTO.PostalCodeId);
                if (ObjDistrict != null)
                {
                    CurrentDistrictDTO = ObjDistrict;
                    Message = "District found";
                }
                else
                {
                    CurrentDistrictDTO = new DistrictDTO(); // empty the textbox fields
                    Message = "District not found";
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
                if (ObjDistrictService.Update(CurrentDistrictDTO))
                {
                    Message = "District updated";
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
                if (ObjDistrictService.Delete(CurrentDistrictDTO.PostalCodeId))
                {
                    Message = "District Deleted";
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

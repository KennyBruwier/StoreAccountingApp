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
    public class DBCountryViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        CountryService ObjCountryService;
        private CountryDTO currentCountryDTO;
        public CountryDTO CurrentCountryDTO
        {
            get { return currentCountryDTO; }
            set { currentCountryDTO = value; OnPropertyChanged("CurrentCountryDTO"); }
        }
        public DBCountryViewModel(INavigationService<HomeViewModel> homeNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            ObjCountryService = new CountryService();
            LoadData();
            CurrentCountryDTO = new CountryDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<CountryDTO> accountTypeList;
        public List<CountryDTO> CountryList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("CountryList"); }
        }
        private void LoadData()
        {
            CountryList = ObjCountryService.GetAll();
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
                var IsSaved = ObjCountryService.Add(CurrentCountryDTO);
                LoadData();
                if (IsSaved)
                    Message = "Country saved";
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
                var ObjCountry = ObjCountryService.Search(CurrentCountryDTO.CountryId);
                if (ObjCountry != null)
                {
                    CurrentCountryDTO = ObjCountry;
                    Message = "Country found";
                }
                else
                {
                    CurrentCountryDTO = new CountryDTO(); // empty the textbox fields
                    Message = "Country not found";
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
                if (ObjCountryService.Update(CurrentCountryDTO))
                {
                    Message = "Country updated";
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
                if (ObjCountryService.Delete(CurrentCountryDTO.CountryId))
                {
                    Message = "Country Deleted";
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

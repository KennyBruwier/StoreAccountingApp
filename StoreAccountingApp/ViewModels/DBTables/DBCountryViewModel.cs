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
    public class DBCountryViewModel : ViewModelBase
    {
        CountryService ObjCountryService;
        private CountryDTO currentCountryDTO;
        public CountryDTO CurrentCountryDTO
        {
            get { return currentCountryDTO; }
            set { currentCountryDTO = value; OnPropertyChanged("CurrentCountryDTO"); }
        }
        public DBCountryViewModel()
        {
            ObjCountryService = new CountryService();
            LoadData();
            CurrentCountryDTO = new CountryDTO();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return ObjCountryService.Add(CurrentCountryDTO);
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
            return ObjCountryService.Update(CurrentCountryDTO);
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
            return ObjCountryService.Delete(CurrentCountryDTO.CountryId);
        }
        #endregion
    }
}

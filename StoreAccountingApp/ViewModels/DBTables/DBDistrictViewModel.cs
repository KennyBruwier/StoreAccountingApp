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
    public class DBDistrictViewModel : ViewModelBase
    {
        DistrictService ObjDistrictService;
        private DistrictDTO currentDistrictDTO;
        public DistrictDTO CurrentDistrictDTO
        {
            get { return currentDistrictDTO; }
            set { currentDistrictDTO = value; OnPropertyChanged("CurrentDistrictDTO"); }
        }
        public DBDistrictViewModel()
        {
            ObjDistrictService = new DistrictService();
            LoadData();
            CurrentDistrictDTO = new DistrictDTO();
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return ObjDistrictService.Add(CurrentDistrictDTO);
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
            return ObjDistrictService.Update(CurrentDistrictDTO);
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
            return ObjDistrictService.Delete(CurrentDistrictDTO.PostalCodeId);
        }
        #endregion
    }
}

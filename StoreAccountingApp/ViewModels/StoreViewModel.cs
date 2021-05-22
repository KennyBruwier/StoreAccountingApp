using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Models;
using StoreAccountingApp.Commands;
using StoreAccountingApp.Models.DTO;

namespace StoreAccountingApp.ViewModels
{
    public class StoreViewModel : INotifyPropertyChanged
    {
        StoreService ObjStoreService;
        private StoreDTO currentStoreDTO;
        public StoreDTO CurrentStoreDTO
        {
            get { return currentStoreDTO; }
            set { currentStoreDTO = value; OnPropertyChanged("CurrentStoreDTO"); }
        }
        public StoreViewModel()
        {
            ObjStoreService = new StoreService();
            LoadData();
            CurrentStoreDTO = new StoreDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region DisplayOperation
        private List<StoreDTO> accountTypeList;
        public List<StoreDTO> StoreList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("StoreList"); }
        }
        private void LoadData()
        {
            StoreList = ObjStoreService.GetAll();
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
                var IsSaved = ObjStoreService.Add(CurrentStoreDTO);
                LoadData();
                if (IsSaved)
                    Message = "Store saved";
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
                var ObjStore = ObjStoreService.Search(CurrentStoreDTO.StoreId);
                if (ObjStore != null)
                {
                    CurrentStoreDTO = ObjStore;
                    Message = "Store found";
                }
                else
                {
                    CurrentStoreDTO = new StoreDTO(); // empty the textbox fields
                    Message = "Store not found";
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
                if (ObjStoreService.Update(CurrentStoreDTO))
                {
                    Message = "Store updated";
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
                if (ObjStoreService.Delete(CurrentStoreDTO.StoreId))
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

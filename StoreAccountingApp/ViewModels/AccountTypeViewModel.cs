using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Models;
using StoreAccountingApp.Commands;

namespace StoreAccountingApp.ViewModels
{
    public class AccountTypeViewModel : INotifyPropertyChanged
    {
        AccountTypeService ObjAccountTypeService;
        private AccountTypeDTO currentAccountTypeDTO;
        public AccountTypeDTO CurrentAccountTypeDTO
        {
            get { return currentAccountTypeDTO; }
            set { currentAccountTypeDTO = value; OnPropertyChanged("CurrentAccountTypeDTO"); }
        }
        public AccountTypeViewModel()
        {
            ObjAccountTypeService = new AccountTypeService();
            LoadData();
            CurrentAccountTypeDTO = new AccountTypeDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region DisplayOperation
        private List<AccountTypeDTO>accountTypeList;
        public List<AccountTypeDTO>AccountTypeList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("AccountTypeList"); }
        }
        private void LoadData()
        {
            AccountTypeList = ObjAccountTypeService.GetAll();
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
                var IsSaved = ObjAccountTypeService.Add(CurrentAccountTypeDTO);
                LoadData();
                if (IsSaved)
                    Message = "AccountType saved";
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
                var ObjAccountType = ObjAccountTypeService.Search(CurrentAccountTypeDTO.AccountTypeId);
                if (ObjAccountType != null)
                {
                    CurrentAccountTypeDTO = ObjAccountType;
                    Message = "AccountType found";
                }
                else
                {
                    CurrentAccountTypeDTO = new AccountTypeDTO(); // empty the textbox fields
                    Message = "AccountType not found";
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
                if (ObjAccountTypeService.Update(CurrentAccountTypeDTO))
                {
                    Message = "AccountType updated";
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
                if (ObjAccountTypeService.Delete(CurrentAccountTypeDTO.AccountTypeId))
                {
                    Message = "AccountType Deleted";
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

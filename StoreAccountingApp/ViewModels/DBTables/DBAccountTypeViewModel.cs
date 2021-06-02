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
using System.Diagnostics;

namespace StoreAccountingApp.ViewModels
{
    public class DBAccountTypeViewModel : DBViewModelBase
    {
        private AccountTypeService ObjAccountTypeService;
        private AccountTypeDTO currentAccountTypeDTO;
        public AccountTypeDTO CurrentAccountTypeDTO
        {
            get { return currentAccountTypeDTO; }
            set { currentAccountTypeDTO = value; OnPropertyChanged("CurrentAccountTypeDTO"); }
        }
        //public ICommand NavigateHomeCommand { get; }
        public DBAccountTypeViewModel()
        {
            //NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            ObjAccountTypeService = new AccountTypeService();
            LoadData();
            CurrentAccountTypeDTO = new AccountTypeDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
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
                Message = CreateValidationErrorMsg(ex);
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
                Message = CreateValidationErrorMsg(ex);
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

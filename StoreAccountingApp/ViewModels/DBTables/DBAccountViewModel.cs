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
    public class DBAccountViewModel : DBViewModelBase
    {
        private readonly AccountService _accountService;
        private AccountDTO currentAccountDTO;
        public AccountDTO CurrentAccountDTO
        {
            get { return currentAccountDTO; }
            set { currentAccountDTO = value; OnPropertyChanged("CurrentAccountDTO"); }
        }
        public DBAccountViewModel()
        {
            _accountService = new AccountService();
            LoadData();
            CurrentAccountDTO = new AccountDTO();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
        #region DisplayOperation
        private List<AccountDTO> accountTypeList;
        public List<AccountDTO> AccountList
        {
            get { return accountTypeList; }
            set { accountTypeList = value; OnPropertyChanged("AccountList"); }
        }
        private void LoadData()
        {
            AccountList = _accountService.GetAll();
        }
        #endregion
        #region SaveOperation
        private readonly RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        public void Save()
        {
            try
            {
                var IsSaved = _accountService.Add(CurrentAccountDTO);
                LoadData();
                if (IsSaved)
                    Message = "Account saved";
                else
                    Message = "Save operation failed";
            }
            catch (DbEntityValidationException ex)
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
        private readonly RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        public void Search()
        {
            try
            {
                var _account = _accountService.Search(CurrentAccountDTO.AccountId);
                if (_account != null)
                {
                    CurrentAccountDTO = _account;
                    Message = "Account found";
                }
                else
                {
                    CurrentAccountDTO = new AccountDTO(); // empty the textbox fields
                    Message = "Account not found";
                }
            }
            catch (DbEntityValidationException ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
        #region UpdateOperation
        private readonly RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                if (_accountService.Update(CurrentAccountDTO))
                {
                    Message = "Account updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (DbEntityValidationException ex)
            {
                Message = CreateValidationErrorMsg(ex);
            }
        }
        #endregion
        #region DeleteOperation
        private readonly RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                if (_accountService.Delete(CurrentAccountDTO.AccountId))
                {
                    Message = "Account Deleted";
                    LoadData();
                }
                else
                    Message = "Delete operation failed";
            }
            catch (DbEntityValidationException ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}

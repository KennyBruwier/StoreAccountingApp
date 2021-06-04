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
using StoreAccountingApp.GeneralClasses;

namespace StoreAccountingApp.ViewModels
{
    public class DBAccountViewModel : ViewModelBase    //DBViewModelBase<AccountDTO,AccountService,Account>
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
            saveCommand = new RelayCommand(SaveAndCatch);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(UpdateAndCatch);
            deleteCommand = new RelayCommand(DeleteAndCatch);
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
        public void SaveAndCatch()
        {
            CatchOperation(Save);
            LoadData();
        }
        public bool Save()
        {
            return _accountService.Add(CurrentAccountDTO);
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
        public void UpdateAndCatch()
        {
            CatchOperation(Update);
            LoadData();
        }
        public bool Update()
        {
            return _accountService.Update(CurrentAccountDTO);
        }
        #endregion
        #region DeleteOperation
        private readonly RelayCommand deleteCommand;

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
            return _accountService.Delete(CurrentAccountDTO.AccountId);
        }
        #endregion
    }
}

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
using StoreAccountingApp.Models;
using StoreAccountingApp.CustomMethods;

namespace StoreAccountingApp.ViewModels
{
    public class DBAccountViewModel : DBViewModelBase<AccountDTO,AccountService,Account>
    {
        private List<ComboboxItem> cbAccountTypeList;
        private List<AccountTypeDTO> _accountTypeList;
        public List<ComboboxItem> CbAccountTypeList
        {
            get { return cbAccountTypeList; }
            set { cbAccountTypeList = value; }
        }
        private List<ComboboxItem> cbEmployeeList;

        public List<ComboboxItem> CbEmployeeList
        {
            get { return cbEmployeeList; }
            set { cbEmployeeList = value; }
        }

        private AccountTypeService _accountTypeService;
        private EmployeeService _employeeService;
        private string unEncryptedPassword;

        public string UnEncryptedPassword
        {
            get { return unEncryptedPassword; }
            set { unEncryptedPassword = value; OnPropertyChanged(nameof(UnEncryptedPassword)); }
        }

        public DBAccountViewModel()
        {
            _accountTypeService = new AccountTypeService();
            _employeeService = new EmployeeService();
            _accountTypeList = _accountTypeService.GetAll();
            CbAccountTypeList = ObjMethods.CreateComboboxList<AccountTypeDTO, ComboboxItem>(
                _accountTypeList,
                "AccountTypeId", "Name");
            CbEmployeeList = ObjMethods.CreateComboboxList<EmployeeDTO, ComboboxItem>(
                _employeeService.GetAll(),
                "EmployeeId", "Firstname", "Lastname");
        }
        
    }
}

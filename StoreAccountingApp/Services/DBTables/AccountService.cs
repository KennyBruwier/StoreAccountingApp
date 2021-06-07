using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Validation;
using StoreAccountingApp.Services.DBTables;
using StoreAccountingApp.GeneralClasses;

namespace StoreAccountingApp.Services
{
    public class AccountService : BaseService<AccountDTO,Account>
    {
        private AccountTypeService _accountTypeService;
        private EmployeeService _employeeService;
        public AccountService()
        {
            _accountTypeService = new AccountTypeService();
            _employeeService = new EmployeeService();
        }
        public override AccountDTO CopyDBtoDTO(Account source)
        {
            AccountDTO newAccountDTO = null;
            if (source != null)
            {
                newAccountDTO = ObjMethods.CopyProperties<Account, AccountDTO>(source);
                if (newAccountDTO.AccountTypeId != 0)
                    newAccountDTO.AccountTypeName = _accountTypeService.Search(newAccountDTO.AccountTypeId).Name;
                if (newAccountDTO.EmployeeId != 0)
                {
                    EmployeeDTO currentEmployeeDTO = _employeeService.Search(newAccountDTO.EmployeeId);
                    newAccountDTO.EmployeeFullname = String.Format("{0} {1}", currentEmployeeDTO.Firstname, currentEmployeeDTO.Lastname);
                }
            }
            return newAccountDTO;
        }
        public bool UserNameExists(string username)
        {
            return ctx.Accounts.FirstOrDefault(a => 
                a.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) != null ;
        }
        public Account LoggedIn(string username, string password)
        {
            return ctx.Accounts.FirstOrDefault(a => 
                (a.Username.Equals(username,StringComparison.OrdinalIgnoreCase) && 
                (a.Password.Equals(password,StringComparison.OrdinalIgnoreCase))));
        }
    }
}

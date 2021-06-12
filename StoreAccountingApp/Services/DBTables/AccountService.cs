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
using System.Security.Cryptography;

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
                {
                    newAccountDTO.AccountTypeDTO = _accountTypeService.Search(newAccountDTO.AccountTypeId);
                    newAccountDTO.AccountTypeName = newAccountDTO.AccountTypeDTO.Name;
                }
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
            Account userAccount = ctx.Accounts.FirstOrDefault(a =>a.Username.Equals(username, StringComparison.OrdinalIgnoreCase)); 
            if (userAccount != null)
            {
                byte[] hashBytes = Convert.FromBase64String(userAccount.Password);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        return null;
            }
            return userAccount;
        }
    }
}

using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Services
{
    public class AccountService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public AccountService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<AccountDTO> GetAll()
        {
            List<AccountDTO> AccountList = new List<AccountDTO>();
            var ObjQuery = from Account in ctx.Accounts
                           select Account;
            foreach (var Account in ObjQuery)
            {
                AccountList.Add(ObjMethods.CopyProperties<Account, AccountDTO>(Account));
            }
            return AccountList;
        }
        public bool Add(AccountDTO newAccountDTO)
        {
            //                                                          <----- Add validations here
            if (newAccountDTO.AccountId != 0)
            {
                if (ctx.Accounts.Find(newAccountDTO.AccountId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An user record with user id {newAccountDTO.AccountId}was already found, do you want to update it instead?",
                                                                    "Account already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newAccountDTO);
                    else
                        throw new ArgumentException($"Add operation failed, user id {newAccountDTO.AccountId}already exists");
                }
            }

            try
            {
                ctx.Accounts.Add(ObjMethods.CopyProperties<AccountDTO, Account>(newAccountDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AccountDTO Search(int userId)
        {
            AccountDTO ObjAccount = null;
            var ObjAccountToFind = ctx.Accounts.Find(userId);
            if (ObjAccountToFind != null)
            {
                ObjAccount = ObjMethods.CopyProperties<Account, AccountDTO>(ObjAccountToFind);
            }
            return ObjAccount;
        }
        public bool LoggedIn(string username, string password)
        {
            bool loggedIn = false;
            var ObjAccountToFind = ctx.Accounts.FirstOrDefault(a=>(a.Username == username) && (a.Password == password));
            if (ObjAccountToFind != null)
                loggedIn = true;
            return loggedIn;
        }
        public bool Update(AccountDTO objAccountToUpdate)
        {
            var ObjAccount = ctx.Accounts.Find(objAccountToUpdate.AccountId);
            if (ObjAccount != null)
            {
                ObjAccount = ObjMethods.CopyProperties<AccountDTO, Account>(objAccountToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int userId)
        {
            var ObjAccountToDelete = ctx.Accounts.Find(userId);
            if (ObjAccountToDelete != null)
                ctx.Accounts.Remove(ObjAccountToDelete);
            return ctx.SaveChanges() > 0;
        }

    }
}

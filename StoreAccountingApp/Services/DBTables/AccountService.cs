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
        public AccountService()
        {

        }

        //public bool Add(AccountDTO newAccountDTO)
        //{
        //    //                                                          <----- Add validations here
        //    if (newAccountDTO.AccountId != 0)
        //    {
        //        if (ctx.Accounts.Find(newAccountDTO.AccountId) != null)
        //        {
        //            MessageBoxResult dialogResult = MessageBox.Show($"An user record with user id {newAccountDTO.AccountId}was already found, do you want to update it instead?",
        //                                                            "Account already exists", MessageBoxButton.YesNo);
        //            if (dialogResult == MessageBoxResult.Yes)
        //                return Update(newAccountDTO);
        //            else
        //                throw new ArgumentException($"Add operation failed, user id {newAccountDTO.AccountId}already exists");
        //        }
        //    }

        //    try
        //    {
        //        ctx.Accounts.Add(ObjMethods.CopyProperties<AccountDTO, Account>(newAccountDTO));
        //        return ctx.SaveChanges() > 0;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //public AccountDTO Search(int userId)
        //{
        //    AccountDTO ObjAccount = null;
        //    var ObjAccountToFind = ctx.Accounts.Find(userId);
        //    if (ObjAccountToFind != null)
        //    {
        //        ObjAccount = ObjMethods.CopyProperties<Account, AccountDTO>(ObjAccountToFind);
        //    }
        //    return ObjAccount;
        //}
        public bool UserNameExists(string username)
        {
            return ctx.Accounts.FirstOrDefault(a => 
                a.Username.Equals(username, StringComparison.OrdinalIgnoreCase)) == null ? false : true ;
        }
        public Account LoggedIn(string username, string password)
        {
            return ctx.Accounts.FirstOrDefault(a => 
                (a.Username.Equals(username,StringComparison.OrdinalIgnoreCase) && 
                (a.Password.Equals(password,StringComparison.OrdinalIgnoreCase))));
        }
        //public bool Update(AccountDTO objAccountToUpdate)
        //{
        //    var ObjAccount = ctx.Accounts.Find(objAccountToUpdate.AccountId);
        //    if (ObjAccount != null)
        //    {
        //        ObjAccount = ObjMethods.CopyProperties<AccountDTO, Account>(objAccountToUpdate);
        //    }
        //    try
        //    {
        //        return ctx.SaveChanges() > 0;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //public bool Delete(int userId)
        //{
        //    var ObjAccountToDelete = ctx.Accounts.Find(userId);
        //    if (ObjAccountToDelete != null)
        //        ctx.Accounts.Remove(ObjAccountToDelete);
        //    return ctx.SaveChanges() > 0;
        //}

    }
}

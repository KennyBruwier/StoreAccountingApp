using StoreAccountingApp.DBModels;
using StoreAccountingApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StoreAccountingApp.Models.Abstracts;

namespace StoreAccountingApp.Models
{
    public class AccountTypeService : ObjMethods
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public AccountTypeService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<AccountTypeDTO>GetAll()
        {
            List<AccountTypeDTO> accountTypeList = new List<AccountTypeDTO>();
            var ObjQuery =  from AccountType in ctx.AccountTypes
                            select AccountType;
            foreach (var accountType in ObjQuery)
            {
                accountTypeList.Add(new AccountTypeDTO(accountTypeId: accountType.AccountTypeId, name: accountType.Name, description: accountType.Description));
            }
            return accountTypeList;
        }
        public bool Add(AccountTypeDTO newAccountType)
        {
            //bool IsAdded = false;
            //                                                          <----- Add validations here
            if (newAccountType.AccountTypeId != 0)
            {
                if (ctx.AccountTypes.Find(newAccountType.AccountTypeId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An account type with id {newAccountType.AccountTypeId} was already found, do you want to update it instead?",
                                                                    "Accountype already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newAccountType);
                    else
                        throw new ArgumentException($"Add operation failed, id {newAccountType.AccountTypeId} already exists");
                }
            }

            if (ctx.AccountTypes.FirstOrDefault(a => a.Name == newAccountType.Name) != null)
                throw new ArgumentException($"Add operation failed, {newAccountType.Name} already exists");
            try
            {
                var objAccountType = CopyProperties<AccountTypeDTO, AccountType>(newAccountType);
                //var objAccountType = new AccountType()
                //{
                //    Name = newAccountType.Name,
                //    Description = newAccountType.Description
                //};
                ctx.AccountTypes.Add(objAccountType);
                //IsAdded = ctx.SaveChanges() > 0;
                return ctx.SaveChanges() > 0; 
            }
            catch (Exception)
            {
                throw;
            }

        }
        public AccountTypeDTO Search(int accountTypeId)
        {
            AccountTypeDTO ObjAccountType = null;
            var ObjAccountTypeToFind = ctx.AccountTypes.Find(accountTypeId);
            if (ObjAccountTypeToFind != null)
            {
                ObjAccountType = CopyProperties<AccountType, AccountTypeDTO>(ObjAccountTypeToFind);
                //ObjAccountType = new AccountTypeDTO()
                //{
                //    AccountTypeId = ObjAccountTypeToFind.AccountTypeId,
                //    Name = ObjAccountTypeToFind.Name,
                //    Description = ObjAccountTypeToFind.Description
                //};
            }
            return ObjAccountType;
        }
        public bool Update(AccountTypeDTO objAccountTypeToUpdate)
        {
            var ObjAccountType = ctx.AccountTypes.Find(objAccountTypeToUpdate.AccountTypeId);
            if (ObjAccountType != null)
            {
                ObjAccountType = CopyProperties<AccountTypeDTO, AccountType>(objAccountTypeToUpdate);
                //ObjAccountType.Name = objAccountTypeToUpdate.Name;
                //ObjAccountType.Description = objAccountTypeToUpdate.Description;
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int accountTypeId)
        {
            var ObjAccountTypeToDelete = ctx.AccountTypes.Find(accountTypeId);
            if (ObjAccountTypeToDelete != null)
                ctx.AccountTypes.Remove(ObjAccountTypeToDelete);
            return ctx.SaveChanges() > 0;
        }
        
    }
}

using StoreAccountingApp.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models
{
    public class AccountTypeService
    {
        _DBStoreAccountingContext ctx;
        public AccountTypeService()
        {
            ctx = new _DBStoreAccountingContext();
        }

        public List<AccountTypeDTO>GetAll()
        {
            List<AccountTypeDTO> accountTypeList = new List<AccountTypeDTO>();
            var ObjQuery = from AccountType in ctx.AccountTypes
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

            var objAccountType = new AccountType()
            {
                Name = newAccountType.Name,
                Description = newAccountType.Description
            };

            ctx.AccountTypes.Add(objAccountType);
            //IsAdded = ctx.SaveChanges() > 0;
            return ctx.SaveChanges() > 0; ;
        }
        public AccountTypeDTO Search(int accountTypeId)
        {
            AccountTypeDTO ObjAccountType = null;
            var ObjAccountTypeToFind = ctx.AccountTypes.Find(accountTypeId);
            if (ObjAccountTypeToFind != null)
            {
                ObjAccountType = new AccountTypeDTO()
                {
                    AccountTypeId = ObjAccountTypeToFind.AccountTypeId,
                    Name = ObjAccountTypeToFind.Name,
                    Description = ObjAccountTypeToFind.Description
                };
            }
            return ObjAccountType;
        }
        public bool Update(AccountTypeDTO objAccountTypeToUpdate)
        {
            var ObjAccountType = ctx.AccountTypes.Find(objAccountTypeToUpdate.AccountTypeId);
            ObjAccountType.Name = objAccountTypeToUpdate.Name;
            ObjAccountType.Description = objAccountTypeToUpdate.Description;
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int accountTypeId)
        {
            var ObjAccountTypeToDelete = ctx.AccountTypes.Find(accountTypeId);
            ctx.AccountTypes.Remove(ObjAccountTypeToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

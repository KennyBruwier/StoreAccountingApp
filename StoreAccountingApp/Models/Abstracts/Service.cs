using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models.Abstracts
{
    public class Service
    {
        protected virtual DTOEntity
        public bool Add(DTOEntity newDTOEntity)
        {
            //bool IsAdded = false;
            //                                                          <----- Add validations here
            var
            if (newDTOEntity.AccountTypeId != 0)
            {
                if (ctx.AccountTypes.Find(newDTOEntity.AccountTypeId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An account type with id {newDTOEntity.AccountTypeId} was already found, do you want to update it instead?",
                                                                    "Accountype already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newDTOEntity);
                    else
                        throw new ArgumentException($"Add operation failed, id {newAccountType.AccountTypeId} already exists");
                }
            }

            if (ctx.AccountTypes.FirstOrDefault(a => a.Name == newDTOEntity.Name) != null)
                throw new ArgumentException($"Add operation failed, {newDTOEntity.Name} already exists");
            try
            {
                var objAccountType = ObjMethods.CopyProperties<DTOEntity, DBEntity>(newDTOEntity);
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
    }
}

using StoreAccountingApp.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models.Interfaces
{
    public class Service<DBEntity, DTOEntity> : IService<DBEntity, DTOEntity>
        where DBEntity : class
        where DTOEntity : class
    {
        protected readonly string _constring = string.Empty;
        System.Data.Entity.DbContext dbc = new DbContext(_constring);
        private _DBStoreAccountingContext ctx;
        public Service(string connectionString)
        {
            _constring = connectionString;
            //ctx = new _DBStoreAccountingContext();
        }

        public bool Add(DTOEntity newDTOEntity)
        {
            //bool IsAdded = false;
            //                                                          <----- Add validations here
            var DBEntityKeys = DBPrimaryKey;
            var DTOKeys = typeof(DTOEntity).GetProperties().Where(x => x.Name.Substring(x.Name.Trim().Length - 2).ToLower() == "id");

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

        public DBEntity Add(DBEntity dBEntity)
        {
            throw new NotImplementedException();
        }

        DTOEntity IService<DBEntity, DTOEntity>.Add(DTOEntity dTOEntity)
        {
            throw new NotImplementedException();
        }

        public object[] DBPrimaryKey
        {
            get
            {
                return (from DBEntity in ctx.Set<DBEntity>().GetType().GetProperties()
                        where Attribute.IsDefined(DBEntity, typeof(KeyAttribute))
                        orderby ((ColumnAttribute)DBEntity.GetCustomAttributes(false).Single(
                            attr => attr is ColumnAttribute)).Order ascending
                        select DBEntity.GetValue(ctx.Set<DBEntity>())).ToArray();
            }
        }
        //public object[] PrimaryKey
        //{
        //    get
        //    {
        //        return (from property in this.GetType().GetProperties()
        //                where Attribute.IsDefined(property, typeof(KeyAttribute))
        //                orderby ((ColumnAttribute)property.GetCustomAttributes(false).Single(
        //                    attr => attr is ColumnAttribute)).Order ascending
        //                select property.GetValue(this)).ToArray();
        //    }
        //}
    }
}

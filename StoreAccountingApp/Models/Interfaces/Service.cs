using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DBModels;
using StoreAccountingApp.DBModels.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Models.Interfaces
{
    public class Service<DBEntity, DTOEntity> : IService<DBEntity, DTOEntity>
        where DBEntity : Entity 
        where DTOEntity : class
    {
        protected readonly string _constring = string.Empty;
        private DbContext dbc;
        private _DBStoreAccountingContext ctx;
        private DBEntity dBEntity;
        public Service(string connectionString)
        {
            _constring = connectionString;
            dbc = new DbContext(_constring);
            //dBEntity = new DBEntity();
            //ctx = new _DBStoreAccountingContext();
        }
        public Service()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public bool Add(DTOEntity newDTOEntity)
        {
            //bool IsAdded = false;
            //                                                          <----- Add validations here

            //var DTOKeys = typeof(DTOEntity).GetProperties().Where(x => x.Name.Substring(x.Name.Trim().Length - 2).ToLower() == "id");
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var DTOProps = typeof(DTOEntity).GetProperties().Where(x => x.CanRead).ToList();
            PropertyInfo currentDBPKs = FindPK;
            PropertyInfo currentDTOPKs = typeof(DBEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));
             

            if (currentDTOPKs.GetValue(newDTOEntity,null) != 0)
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
        private bool PropNotEmpty(DTOEntity newDTOEntity)
        {
            var DBKeyName = FindPK.Name;
            DBKeys.
            foreach (PropertyInfo property in DBKeys)
            {
                if (ObjMethods.IsNumericType(property) && (int)property.GetValue(source, null) == 0)
                    bContinue = false;

                if (property.)
            }
        }
        //private object[] PK_GetByProperties2
        //{
        //    get
        //    {
        //        return (from DBEntity in ctx.Set<DBEntity>().GetType().GetProperties()
        //                where Attribute.IsDefined(DBEntity, typeof(KeyAttribute))
        //                orderby ((ColumnAttribute)DBEntity.GetCustomAttributes(false).Single(
        //                    attr => attr is ColumnAttribute)).Order ascending
        //                select DBEntity.GetValue(ctx.Set<DBEntity>())).ToArray();
        //    }
        //}
        private PropertyInfo findProperty(string propertyName)
        {
            

        }
        private PropertyInfo PK_GetByProperties
        {
            get
            {
                return typeof(DBEntity).GetProperties().FirstOrDefault(p =>     p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase) || 
                                                                                p.Name.Equals(typeof(DBEntity).Name + "ID", StringComparison.OrdinalIgnoreCase));
            }
        }
        private PropertyInfo PK_GetByKeyAttritbutes
        {
            get
            {
                return typeof(DBEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));
            }
        }
        private IEnumerable<string> K_NamesGetByFluentAPI
        {
            get
            {
                DbContext dbContext = dbc != null ? dbc : ctx;
                IEnumerable<string> keys = null;
                if (dbContext != null)
                {
                    ObjectContext objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
                    ObjectSet<DBEntity> set = objectContext.CreateObjectSet<DBEntity>();
                    keys = set.EntitySet.ElementType.KeyMembers.Select(k=>k.Name);
                }
                return keys;
            }
        }
        private PropertyInfo PK_GetByFluentAPI
        {
            get
            {
                return K_GetByFluentAPI.FirstOrDefault();
            }
        }
        private PropertyInfo[] K_GetByFluentAPI
        {
            get
            {
                IEnumerable<string> keyNames = K_NamesGetByFluentAPI;
                List<PropertyInfo> keys = new List<PropertyInfo>();
                foreach (var keyName in keyNames)
                {
                    keys.Add(typeof(DBEntity).GetProperties().FirstOrDefault(p => p.Name == keyName));
                }
                return keys.ToArray();
            }
        }
        private PropertyInfo FindPK
        {
            get
            {
                PropertyInfo PKfound = null;
                PKfound = PK_GetByProperties;
                if (PKfound == null)
                    PKfound = PK_GetByKeyAttritbutes;
                if (PKfound == null)
                    PKfound = PK_GetByFluentAPI;
                return PKfound;
            }
        }
    }
}

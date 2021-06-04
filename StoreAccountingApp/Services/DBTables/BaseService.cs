using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DTO;
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Models;
using StoreAccountingApp.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Services.DBTables
{
    public abstract class BaseService<DTOModel, DBModel> : ErrorHandling, IBaseService<DTOModel, DBModel>
        where DTOModel : BaseDTO, new()
        where DBModel : BaseModel
    {
        protected _DBStoreAccountingContext ctx;
        public BaseService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public bool Add(DBModel recordToAdd)
        {
            ctx.Set<DBModel>().Add(recordToAdd);
            return ctx.SaveChanges() > 0;
        }

        public bool Delete(object[] idToDelete)
        {
            var recordToDelete = ctx.Set<DBModel>().Find(idToDelete);
            if (recordToDelete != null)
                ctx.Set<DBModel>().Remove(recordToDelete);
            return ctx.SaveChanges() > 0;
        }

        public List<DTOModel> GetAll()
        {
            List<DTOModel> recordList = new List<DTOModel>();
            var ObjQuery = ctx.Set<DBModel>().ToList();
            foreach (var record in ObjQuery)
            {
                recordList.Add(ObjMethods.CopyProperties<DBModel, DTOModel>(record));
            }
            return recordList;
        }
        public DTOModel Search(object[] idToSearch)
        {
            DTOModel currentDTOModel = null;
            var recordFound = ctx.Set<DBModel>().Find(idToSearch);
            if (recordFound != null)
            {
                currentDTOModel = ObjMethods.CopyProperties<DBModel, DTOModel>(recordFound);
            }
            return currentDTOModel;
        }
        public bool Update(DBModel recordToUpdate)
        {
            var recordFound = ctx.Set<DBModel>().Find(recordToUpdate.PrimaryKey);
            if (recordFound != null)
            {
                recordFound = recordToUpdate;
            }
            else
                return false;
            try
            {
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PropertyInfo PK_GetByProperties
        {
            get
            {
                return typeof(DBModel).GetProperties().FirstOrDefault(p => p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase) ||
                                                                                p.Name.Equals(typeof(DBModel).Name + "ID", StringComparison.OrdinalIgnoreCase));
            }
        }
        private PropertyInfo PK_GetByKeyAttritbutes
        {
            get
            {
                return typeof(DBModel).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));
            }
        }
        private IEnumerable<string> K_NamesGetByFluentAPI
        {
            get
            {
                // DbContext ctx = dbc != null ? dbc : ctx;
                IEnumerable<string> keys = null;
                if (ctx != null)
                {
                    ObjectContext objectContext = ((IObjectContextAdapter)ctx).ObjectContext;
                    ObjectSet<DBModel> set = objectContext.CreateObjectSet<DBModel>();
                    keys = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name);
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
                    keys.Add(typeof(DBModel).GetProperties().FirstOrDefault(p => p.Name == keyName));
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

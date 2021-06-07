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
        where DBModel : BaseModel, new()
    {
        protected DBStoreAccountingContext ctx;
        public BaseService()
        {
            ctx = new DBStoreAccountingContext();
        }
        public virtual DTOModel CopyDBtoDTO(DBModel source)
        {
            return ObjMethods.CopyProperties<DBModel, DTOModel>(source);
        }
        public virtual DBModel CopyDTOtoDB(DTOModel source)
        {
            return ObjMethods.CopyProperties<DTOModel, DBModel>(source);
        }
        public bool Add(DTOModel dtoModelToAdd)
        {
            DBModel dBModel = CopyDTOtoDB(dtoModelToAdd);
            ctx.Set<DBModel>().Add(CopyDTOtoDB(dtoModelToAdd));
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(params object[] idToDelete)
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
                recordList.Add(CopyDBtoDTO(record));
            }
            return recordList;
        }
        public List<DTOModel> GetAll(params object[] rangeIdsToSearch)
        {
            List<DTOModel> recordList = new List<DTOModel>();
            foreach (object idToSearch in rangeIdsToSearch)
            {
                DTOModel recordFound = Search(idToSearch);
                if (recordFound != null)
                {
                    recordList.Add(recordFound);
                }
            }
            return recordList;
        }
        public DTOModel Search(object[] idToSearch)
        {
            DTOModel currentDTOModel = null;
            var recordFound = ctx.Set<DBModel>().Find(idToSearch);
            if (recordFound != null)
            {
                currentDTOModel = CopyDBtoDTO(recordFound);
            }
            return currentDTOModel;
        }
        public DTOModel Search(object idToSearch)
        {
            DTOModel currentDTOModel = null;
            var recordFound = ctx.Set<DBModel>().Find(idToSearch);
            if (recordFound != null)
            {
                currentDTOModel = CopyDBtoDTO(recordFound);
            }
            return currentDTOModel;
        }
        public DTOModel Search(DBField dbFieldToSearch)
        {
            DTOModel currentDTOModel = null;
            if ((dbFieldToSearch != null) &&
                    (dbFieldToSearch.Name != null) &&
                    (dbFieldToSearch.Name.Length > 0)
                )
            {
                PropertyInfo property = typeof(DTOModel).GetProperty(dbFieldToSearch.Name);
                if (property != null)
                {
                    var recordFound = ctx.Set<DBModel>().FirstOrDefault(d => property.GetValue(d, null) == dbFieldToSearch.Value);
                    if (recordFound != null)
                    {
                        currentDTOModel = CopyDBtoDTO(recordFound);
                    }
                }
            }
            return currentDTOModel;
        }
        public DTOModel Search(DBField[] dbFieldsToSearch)
        {
            DTOModel currentDTOModel = null;
            List<PropertyInfo> properties = null;
            foreach (DBField field in dbFieldsToSearch)
            {
                if ((field != null) &&
                        (field.Name != null) &&
                        (field.Name.Length > 0)
                    )
                {
                    if (properties == null) properties = new List<PropertyInfo>();
                    properties.Add(typeof(DTOModel).GetProperty(field.Name));
                }
            }
            if (properties != null)
            {
                var recordsFound = ctx.Set<DBModel>().Where(d => properties[0].GetValue(d, null) == dbFieldsToSearch[0].Value).DefaultIfEmpty();
                if (properties.Count > 1)
                    for (int i = 1; i < properties.Count - 1; i++)
                    {
                        if (recordsFound != null)
                            recordsFound = recordsFound.Where(d => properties[i].GetValue(d, null) == dbFieldsToSearch[i].Value).DefaultIfEmpty();
                        else
                            break;
                    }
                if (recordsFound != null)
                {
                    currentDTOModel = CopyDBtoDTO(recordsFound.FirstOrDefault());
                }
            }
            return currentDTOModel;
        }
        public string Search(object[] idToSearch, params string[] columnNamesToReturn)
        {
            string toReturn = String.Empty;
            if (idToSearch != null)
            {
                toReturn = ReturnColumnsValue(Search(idToSearch),columnNamesToReturn);
            }
            return toReturn;
        }
        public string Search(object idToSearch, params string[] columnNamesToReturn)
        {
            string toReturn = String.Empty;
            if (idToSearch != null)
            {
                toReturn = ReturnColumnsValue(Search(idToSearch), columnNamesToReturn);
            }
            return toReturn;
        }
        public bool Update(DTOModel dtoModelToUpdate)
        {
            DBModel recordFound = ctx.Set<DBModel>().Find(dtoModelToUpdate.Validation.GetPrimaryKeysValue());
            if (recordFound != null)
            {
                recordFound = CopyDTOtoDB(dtoModelToUpdate);
                
                return ctx.SaveChanges() > 0;
            }
            else
                return false;
            //try
            //{
            //    return ctx.SaveChanges() > 0;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        private string ReturnColumnsValue(DTOModel record, params string[] columnNamesToReturn)
        {
            string toReturn = String.Empty;
            if (record != null)
            {
                var sourceProps = typeof(DTOModel).GetProperties().Where(x => x.CanRead).ToList();
                int iCount = 0;
                foreach (PropertyInfo property in sourceProps)
                {
                    if (columnNamesToReturn.Contains(property.Name))
                        toReturn += (iCount++ == 0 ? "" : " ") + property.GetValue(record, null).ToString();
                }
            }
            return toReturn;
        }
    }
}

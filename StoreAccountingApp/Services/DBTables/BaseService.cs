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
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
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
            //List<string> ids= new List<string>();
            //foreach (var item in idToSearch)
            //{
            //    ids.Add(item.ToString());
            //}
            var recordFound = ctx.Set<DBModel>().Find(idToSearch.ToArray()); //idToSearch.ToArray()
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
                var recordFound = ctx.Set<DBModel>().FirstOrDefault((Expression<Func<DBModel, bool>>)BuildLambaExpression<DBModel>(dbFieldToSearch));
                if (recordFound != null)
                    currentDTOModel = CopyDBtoDTO(recordFound);
            }
            return currentDTOModel;
        }
        public DTOModel Search(DBField[] dbFieldsToSearch)
        {
            DTOModel currentDTOModel = null;
            var recordFound = ctx.Set<DBModel>().FirstOrDefault((Expression<Func<DBModel, bool>>)BuildLambaExpression<DBModel>(dbFieldsToSearch));
            if (recordFound != null)
            {
                currentDTOModel = CopyDBtoDTO(recordFound);
            }
            return currentDTOModel;
        }
        public DTOModel Search(DBField[][] dbFieldsToSearch)
        {
            DTOModel currentDTOModel = null;
            foreach (DBField[] field in dbFieldsToSearch)
            {
                DTOModel recordFound = Search(field);
                if (recordFound != null)
                {
                    currentDTOModel = recordFound;
                    break;
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
                //recordFound = CopyDTOtoDB(dtoModelToUpdate);
                ctx.Entry(recordFound).CurrentValues.SetValues(CopyDTOtoDB(dtoModelToUpdate));
                return ctx.SaveChanges() > 0;
            }
            else
                return false;
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
        public Expression BuildLambaExpression<Model>(string fieldName, object fieldValues)
        {
            var parameter = Expression.Parameter(typeof(DBModel), "p");
            return Expression.Lambda<Func<DBModel, bool>>(
                 Expression.Equal(Expression.PropertyOrField(parameter, fieldName), Expression.Constant(fieldValues)),
                parameter);
        }
        public Expression BuildLambaExpression<Model>(params DBField[] dBFields)
        {
            var parameter = Expression.Parameter(typeof(DBModel), "p");
            Expression expression = null;
            for (int i = 0; i < dBFields.Length; i++)
            {
                if (i == 0) expression = Expression.Equal(Expression.PropertyOrField(parameter, dBFields[i].Name), Expression.Constant(dBFields[i].Value));
                else expression = Expression.AndAlso(expression, Expression.Equal(Expression.PropertyOrField(parameter, dBFields[i].Name), Expression.Constant(dBFields[i].Value)));
            }
            return Expression.Lambda<Func<DBModel, bool>>(expression,
                parameter);
        }
    }
}

using StoreAccountingApp.DTO;
using StoreAccountingApp.DTO.Abstracts;
using StoreAccountingApp.Models;
using StoreAccountingApp.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.CustomMethods
{
    public static class ObjMethods
    {
        public static TU CopyProperties<T, TU>(T source) where TU : new()
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties().Where(x => x.CanWrite).ToList();
            TU dest = new TU();
            foreach (PropertyInfo sourceProp in sourceProps)
            {
                bool bContinue = true;
                PropertyInfo p = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (p != null)
                {
                    //if ((sourceProp.PropertyType != typeof(BaseModel)) && (sourceProp.PropertyType != typeof(BaseDTO)))
                    //{
                    if (sourceProp.Name.Substring(sourceProp.Name.Trim().Length - 2).ToLower() == "id")
                    {
                        if (IsNumericType(sourceProp) && (int)sourceProp.GetValue(source, null) == 0)
                            bContinue = false;
                    }
                    if (bContinue)
                    {
                        if (p.CanWrite)
                        {
                            if ((sourceProp.PropertyType != typeof(BaseModel)) && (sourceProp.PropertyType != typeof(BaseDTO)))
                                p.SetValue(dest, sourceProp.GetValue(source, null), null);
                        }
                    }
                    //}
                    //if ((sourceProp.PropertyType == typeof(BaseModel)) && (p.PropertyType == typeof(BaseDTO)))
                    //{
                    //    p = sourceProp;
                    //}
                }
            }
            return dest;
        }
        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static PropertyInfo[] FindIds<T>() where T:new()
        {
            return (PropertyInfo[])typeof(T).GetProperties().Where(x => x.Name.Substring(x.Name.Trim().Length - 2).ToLower() == "id");
        }
        //public static object[] DBPrimaryKey<T>
        //{
        //    get
        //    {
        //        _DBStoreAccountingContext ctx = new _DBStoreAccountingContext();

        //        return (from DBEntity in ctx.Set<DBEntity>().GetType().GetProperties()
        //                where Attribute.IsDefined(DBEntity, typeof(KeyAttribute))
        //                orderby ((ColumnAttribute)DBEntity.GetCustomAttributes(false).Single(
        //                    attr => attr is ColumnAttribute)).Order ascending
        //                select DBEntity.GetValue(ctx.Set<DBEntity>())).ToArray();
        //    }
        //}
    }
}

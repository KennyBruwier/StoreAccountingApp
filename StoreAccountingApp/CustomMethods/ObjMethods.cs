using StoreAccountingApp.DTO;
using StoreAccountingApp.DTO.Abstracts;
using StoreAccountingApp.Models;
using StoreAccountingApp.Models.Abstracts;
using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.CustomMethods
{
    internal enum ComboFields
    {
        NotSelected,
        Key,
        Text
    }
    public static class ObjMethods
    {
        public static List<TU> CreateComboboxList<T, TU>(List<T> dtoList, string propertyKey, params string[] propertiesToInclude)
            where T : BaseDTO
            where TU: ComboboxItem, new()
        {
            List<TU> comboboxItems = new List<TU>();
            var dtoListProperties = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            foreach (var dtoItem in dtoList)
            {
                TU comboboxItem = null;
                int iCountText = 0;
                foreach (PropertyInfo property in dtoListProperties)
                {
                    ComboFields includeAs = ComboFields.NotSelected;
                    object dtoItemValue = property.GetValue(dtoItem, null);
                    var propertyName = property.Name;
                    if (dtoItemValue != null)
                    {
                        if ((dtoItemValue is int) && (property.Name == propertyKey))
                            includeAs = ComboFields.Key;
                        if (propertiesToInclude.Contains(property.Name))
                            includeAs = ComboFields.Text;
                    }
                    if (includeAs != ComboFields.NotSelected)
                        if (comboboxItem == null) comboboxItem = new TU();
                        if (includeAs == ComboFields.Key) comboboxItem.Key = (int)property.GetValue(dtoItem, null);
                        if (includeAs == ComboFields.Text) comboboxItem.Text += iCountText++ > 0 ? " " : "" + property.GetValue(dtoItem, null).ToString();
                }
                if (comboboxItem != null)
                    comboboxItems.Add(comboboxItem);
            }
            return comboboxItems;
        }
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
                    if (!(sourceProp.PropertyType.IsClass && 
                        (sourceProp.PropertyType.Assembly.FullName == typeof(T).Assembly.FullName)))
                    {
                        var sourcePropValue = sourceProp.GetValue(source, null);
                        if (sourceProp.Name.Substring(sourceProp.Name.Trim().Length - 2).ToLower() == "id")
                        {
                            if (sourcePropValue is int @int && @int == 0)
                                bContinue = false;
                        }
                        if (bContinue)
                        {
                            if (p.CanWrite)
                            {
                                //if (sourceProp.PropertyType.IsClass)
                                //{
                                //    if (typeof(T) == typeof(BaseModel))
                                //    {
                                //    }
                                //    //if (
                                //    //    (sourceProp.PropertyType.Assembly.FullName == typeof(T).Assembly.FullName) || 
                                //    //    (sourceProp.PropertyType.Assembly.FullName == typeof(TU).Assembly.FullName)
                                //    //   )
                                //}
                                //else
                                p.SetValue(dest, sourceProp.GetValue(source, null), null);
                            }
                        }
                    }
                }
            }
            return dest;
        }
        public static bool IsStringType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.String: return true;
                default: return false;
            }
        }
        public static bool IsNumericType(object o)
        {
            var temp = Type.GetTypeCode(o.GetType()).ToString();
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

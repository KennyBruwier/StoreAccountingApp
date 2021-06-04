using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.CustomMethods;

namespace StoreAccountingApp.GeneralClasses
{
    public class CheckValidation
    {
        public bool EmptyPK 
        { 
            get
            {
                return CheckIfPKEmpty();
            }    
        }
        public bool NullFields
        {
            get
            {
                return CheckNonNullableFields();
            }
        }
        private List<DBField> PrimaryKeys { get; set; }
        private List<DBField> NonNullableFields { get; set; }
        public string ValidationErrors { get; set; } = String.Empty;
        private bool CheckIfPKEmpty()
        {
            bool PKEmpty = false;
            if (PrimaryKeys != null)
            {
                foreach (var key in PrimaryKeys)
                {
                    string emptyKey = String.Empty;
                    if (key.Value == null || 
                        (ObjMethods.IsNumericType(key.Value) && (Convert.ToInt32(key.Value) == 0)) ||
                        (ObjMethods.IsStringType(key.Value) && (key.Value.ToString() == "")))
                        emptyKey = String.Format("Primary Key {0} is empty",key.Name);
                    if (emptyKey != String.Empty)
                    {
                        PKEmpty = true;
                        BuildValidationMsg(emptyKey);
                    }
                }
            }
            return PKEmpty;
        }
        private bool CheckNonNullableFields()
        {
            bool nullFieldsFound = false;
            if (NonNullableFields != null)
            {
                foreach (var NonNullableField in NonNullableFields)
                {
                    if (NonNullableField.Value == null)
                    {
                        BuildValidationMsg(String.Format("A non nullable field {0} is null",NonNullableField.Name));
                        nullFieldsFound = true;
                    }
                }
            }
            return nullFieldsFound;
        }
        private void BuildValidationMsg(string msg)
        {
            if (ValidationErrors == String.Empty)
                ValidationErrors = msg;
            else
                ValidationErrors += '\n' + msg;
        }
        public void AddPrimaryKey(string fieldName, object fieldValue)
        {
            if (PrimaryKeys == null) PrimaryKeys = new List<DBField>();
            PrimaryKeys.Add(new DBField(fieldName, fieldValue));
        }
        public void AddNonNullFields(string fieldName, object fieldValue)
        {
            if (NonNullableFields == null) NonNullableFields = new List<DBField>();
            NonNullableFields.Add(new DBField(fieldName, fieldValue));
        }
        public object[] PrimaryKeysValue()
        {
            List<object> keysValue = null;
            if (PrimaryKeys!=null)
            {
                keysValue = new List<object>();
                foreach (DBField key in PrimaryKeys)
                {
                    keysValue.Add(key.Value);
                }
            }
            if (keysValue != null)
                return keysValue.ToArray();
            else
                return null;
        }
    }
}

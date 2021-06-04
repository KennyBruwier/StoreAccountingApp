using StoreAccountingApp.GeneralClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DTO
{
    public class BaseDTO : INotifyPropertyChanged
    {
        private CheckValidation validation;
        public CheckValidation Validation
        {
            get { return validation; }
            set 
            { 
                validation = value; 
                OnPropertyChanged(nameof(Validation)); 
            }
        }
        public virtual void LoadValidation() { }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
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

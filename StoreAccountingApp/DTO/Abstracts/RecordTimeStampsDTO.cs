using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StoreAccountingApp.DTO.Abstracts
{
    public abstract class RecordTimeStampsDTO : INotifyPropertyChanged
    {
        private DateTime createdAt = DateTime.Now;
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; OnPropertyChanged("CreatedAt"); }
        }
        private DateTime updateAt = DateTime.Now;
        public DateTime UpdateAt
        {
            get { return updateAt; }
            set { updateAt = value; OnPropertyChanged("UpdateAt"); }
        }
        private DateTime? closedAt;
        public DateTime? ClosedAt
        {
            get { return closedAt; }
            set { closedAt = value; OnPropertyChanged("ClosedAt"); }
        }
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

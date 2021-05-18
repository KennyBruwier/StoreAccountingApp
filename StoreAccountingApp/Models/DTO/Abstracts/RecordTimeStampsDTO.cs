using System;
using System.ComponentModel;

namespace StoreAccountingApp.Models.DTO.Abstracts
{
    public abstract class RecordTimeStampsDTO : INotifyPropertyChanged
    {
        private DateTime createdAt;
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; OnPropertyChanged("CreatedAt"); }
        }
        private DateTime updateAt;
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
    }
}

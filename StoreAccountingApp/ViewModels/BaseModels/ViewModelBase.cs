using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace StoreAccountingApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public virtual void Dispose() { }
    }
}

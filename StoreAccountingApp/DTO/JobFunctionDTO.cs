using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StoreAccountingApp.DTO
{
    public class JobFunctionDTO : INotifyPropertyChanged
    {
        private int jobFunctionId;
        public int JobFunctionId
        {
            get { return jobFunctionId; }
            set { jobFunctionId = value; OnPropertyChanged("JobFunctionId"); }
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged("Category"); }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

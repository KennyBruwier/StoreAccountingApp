using System;

namespace StoreAccountingApp.DTO.Abstracts
{
    public abstract class ServicePersonCredentialsDTO : PersonCredentialsDTO
    {
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        private DateTime inService;
        public DateTime InService
        {
            get { return inService; }
            set { inService = value; OnPropertyChanged("InService"); }
        }
        private DateTime? outOfService;
        public DateTime? OutOfService
        {
            get { return outOfService; }
            set { outOfService = value; OnPropertyChanged("OutOfService"); }
        }

    }
}

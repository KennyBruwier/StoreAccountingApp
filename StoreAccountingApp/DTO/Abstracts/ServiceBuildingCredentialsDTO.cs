using System;
using System.Collections.Generic;

namespace StoreAccountingApp.DTO.Abstracts
{
    public abstract class ServiceBuildingCredentialsDTO : AddressDTO
    {
        private string buildingName;
        public string BuildingName
        {
            get { return buildingName; }
            set { buildingName = value; OnPropertyChanged("BuildingName"); }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        private DateTime inService = DateTime.Now;
        public DateTime InService
        {
            get { return inService; }
            set { inService = value; OnPropertyChanged("InService"); }
        }
        private DateTime outOfService;

        public DateTime OutOfService
        {
            get { return outOfService; }
            set { outOfService = value; OnPropertyChanged("OutOfService"); }
        }

    }
}

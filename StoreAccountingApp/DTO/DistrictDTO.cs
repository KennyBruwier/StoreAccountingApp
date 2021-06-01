﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DTO
{
    public class DistrictDTO : INotifyPropertyChanged
    {
        private string postalCodeId;
        public string PostalCodeId
        {
            get { return postalCodeId; }
            set { postalCodeId = value; OnPropertyChanged("PostalCodeId"); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private int countryId;
        public int CountryId
        {
            get { return countryId; }
            set { countryId = value; OnPropertyChanged("CountryId"); }
        }
        private CountryDTO countryDTO;
        public CountryDTO CountryDTO
        {
            get { return countryDTO; }
            set { countryDTO = value; OnPropertyChanged("CountryDTO"); }
        }

        //private int countryName;
        //public int CountryName
        //{
        //    get { return countryName; }
        //    set { countryName = value; OnPropertyChanged("CountryName"); }
        //}
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

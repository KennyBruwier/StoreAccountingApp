﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StoreAccountingApp.Models.DTO.Abstracts
{
    public abstract class AddressDTO : AddressDigitalDTO
    {
        private string streetname;
        public string Streetname
        {
            get { return streetname; }
            set { streetname = value; OnPropertyChanged("Streetname"); }
        }
        private string houseNr;
        public string HouseNr
        {
            get { return houseNr; }
            set { houseNr = value; OnPropertyChanged("HouseNr"); }
        }
        private string boxNr;
        public string BoxNr
        {
            get { return boxNr; }
            set { boxNr = value; OnPropertyChanged("BoxNr"); }
        }
        private string postalCodeId;
        public string PostalCodeId
        {
            get { return postalCodeId; }
            set { postalCodeId = value; OnPropertyChanged("PostalCodeId"); }
        }
        private string faxNumber;
        public string FaxNumber
        {
            get { return faxNumber; }
            set { faxNumber = value; OnPropertyChanged("FaxNumber"); }
        }

    }
}
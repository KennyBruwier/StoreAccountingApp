using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Models.DTO
{
    public class CountryDTO : INotifyPropertyChanged
    {
        private int countryId;
        public int CountryId
        {
            get { return countryId; }
            set { countryId = value; OnPropertyChanged("CountryId"); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string iso3166Code;
        public string Iso3166Code
        {
            get { return iso3166Code; }
            set { iso3166Code = value; OnPropertyChanged("Iso3166Code"); }
        }
        private string capital;
        public string Capital
        {
            get { return capital; }
            set { capital = value; OnPropertyChanged("Capital"); }
        }
        private string phoneCode;
        public string PhoneCode
        {
            get { return phoneCode; }
            set { phoneCode = value; OnPropertyChanged("PhoneCode"); }
        }
        private int timeDiff_UTC;
        public int TimeDiff_UTC
        {
            get { return timeDiff_UTC; }
            set { timeDiff_UTC = value; OnPropertyChanged("TimeDiff_UTC"); }
        }
        public CountryDTO(int countryId, string name, string iso3166Code, string capital, string phoneCode, int timeDiff_UTC)
        {
            CountryId = countryId;
            Name = name;
            Iso3166Code = iso3166Code;
            Capital = capital;
            PhoneCode = phoneCode;
            TimeDiff_UTC = timeDiff_UTC;
        }
        public CountryDTO()
        {

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

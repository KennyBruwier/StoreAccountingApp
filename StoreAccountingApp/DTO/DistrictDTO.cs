using StoreAccountingApp.DTO.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DTO
{
    public class DistrictDTO : RecordTimeStampsDTO
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
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(PostalCodeId), PostalCodeId);
            Validation.AddNonNullFields(nameof(Name), Name);
            Validation.AddNonNullFields(nameof(CountryId), CountryId);
        }
    }
}

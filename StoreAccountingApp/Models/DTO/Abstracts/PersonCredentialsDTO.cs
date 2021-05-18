using System;

namespace StoreAccountingApp.Models.DTO.Abstracts
{
    public abstract class PersonCredentialsDTO : AddressDTO
    {
        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; OnPropertyChanged("Firstname"); }
        }
        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; OnPropertyChanged("Lastname"); }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged("Gender"); }
        }
        private DateTime? birthday;
        public DateTime? Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }
    }
}

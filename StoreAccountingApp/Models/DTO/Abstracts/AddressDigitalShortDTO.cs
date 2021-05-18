
namespace StoreAccountingApp.Models.DTO.Abstracts
{
    public abstract class AddressDigitalShortDTO : RecordTimeStampsDTO
    {
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; OnPropertyChanged("EmailAddress"); }
        }
    }
}

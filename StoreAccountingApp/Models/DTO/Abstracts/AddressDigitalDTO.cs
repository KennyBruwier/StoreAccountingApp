
namespace StoreAccountingApp.Models.DTO.Abstracts
{
    public abstract class AddressDigitalDTO : AddressDigitalShortDTO
    {
        private string website;
        public string Website
        {
            get { return website; }
            set { website = value; OnPropertyChanged("Website"); }
        }
        private string facebook;
        public string Facebook
        {
            get { return facebook; }
            set { facebook = value; OnPropertyChanged("Facebook"); }
        }
        private string linkedIn;
        public string LinkedIn
        {
            get { return linkedIn; }
            set { linkedIn = value; OnPropertyChanged("Linkedin"); }
        }
    }
}

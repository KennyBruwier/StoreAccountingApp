using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.DTO.Abstracts;
using StoreAccountingApp.GeneralClasses;

namespace StoreAccountingApp.DTO
{
    public class ClientDTO : PersonCredentialsDTO
    {
        private int clientId;
        public int ClientId
        {
            get { return clientId; }
            set { clientId = value; OnPropertyChanged("ClientId"); }
        }
        private string organisation;
        public string Organisation
        {
            get { return organisation; }
            set { organisation = value; OnPropertyChanged("Organisation"); }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged("Category"); }
        }
        private string customerCard;
        public string CustomerCard
        {
            get { return customerCard; }
            set { customerCard = value; OnPropertyChanged("CustomerCard"); }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        public ClientDTO(int clientId, string organistaion, string category, string customercard, string firstname, string lastname, string gender, string status, string streetname, string houseNr, string boxNr, string postalCodeId, string faxNumber, string phoneNumber, string emailAddress, string website, string facebook, string linkedin, DateTime? birthday)
        {
            Category = category;
            Organisation = organistaion;
            CustomerCard = customercard;
            Gender = gender;
            Status = status;
            Streetname = streetname;
            HouseNr = houseNr;
            BoxNr = boxNr;
            PostalCodeId = postalCodeId;
            FaxNumber = faxNumber;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Website = website;
            Facebook = facebook;
            LinkedIn = linkedin;
            Birthday = birthday;
        }
        public ClientDTO(DateTime closedAt)
        {
            ClosedAt = closedAt;
        }
        public ClientDTO()
        {
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(ClientId), ClientId);
            Validation.AddNonNullFields(nameof(Firstname), Firstname);
            Validation.AddNonNullFields(nameof(Lastname), Lastname);
            Validation.AddNonNullFields(nameof(EmailAddress), EmailAddress);
            List<DBField> dBFields = new List<DBField>();
            dBFields.Add(new DBField() { Name = nameof(Firstname), Value = Firstname });
            dBFields.Add(new DBField() { Name = nameof(Lastname), Value = Lastname });
            Validation.AddUniqueValueFields(dBFields.ToArray());
        }
    }
}

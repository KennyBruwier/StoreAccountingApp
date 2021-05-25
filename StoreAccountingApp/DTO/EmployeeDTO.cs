using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class EmployeeDTO : ServicePersonCredentialsDTO
    {
        private int employeeId;
        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; OnPropertyChanged("EmployeeId"); }
        }
        public EmployeeDTO(int employeeId, string firstname, string lastname, string gender, string status, string streetname, string houseNr, string boxNr, string postalCodeId, string faxNumber, string phoneNumber, string emailAddress, string website, string facebook, string linkedin, DateTime? birthday) 
        {
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
        public EmployeeDTO(DateTime? outOfService, DateTime closedAt)
        {
            OutOfService = outOfService;
            ClosedAt = closedAt;
        }
        public EmployeeDTO()
        {

        }
    }
}

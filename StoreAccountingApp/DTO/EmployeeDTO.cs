using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;
using StoreAccountingApp.GeneralClasses;

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

        private string jobFunctionTitles;

        public string JobFunctionTitles
        {
            get { return jobFunctionTitles; }
            set { jobFunctionTitles = value; OnPropertyChanged("JobFunctionTitles"); }
        }

        public EmployeeDTO(int employeeId, string firstname, string lastname, string gender, string status, string streetname, string houseNr, string boxNr, string postalCodeId, string faxNumber, string phoneNumber, string emailAddress, string website, string facebook, string linkedin, DateTime? birthday) 
        {
            Firstname = firstname;
            Lastname = lastname;
            EmployeeId = employeeId;
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
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(EmployeeId), EmployeeId);
            Validation.AddNonNullFields(nameof(Firstname), Firstname);
            Validation.AddNonNullFields(nameof(Lastname), Lastname);
            Validation.AddNonNullFields(nameof(EmailAddress), EmailAddress);
            //Validation.AddUniqueValueFields(nameof(Firstname), Firstname);
            //Validation.AddUniqueValueFields(nameof(Lastname), Lastname);
            Validation.AddUniqueValueFields
                (
                    new DBField[]
                    {   
                        new DBField() { Name = nameof(Firstname), Value = Firstname },
                        new DBField() { Name = nameof(Lastname), Value = Lastname   }
                    }
                );
            Validation.AddUniqueValueFields(nameof(EmailAddress), EmailAddress);
        }
    }
}

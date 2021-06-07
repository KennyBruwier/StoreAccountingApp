using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;
using StoreAccountingApp.Models;

namespace StoreAccountingApp.DTO
{
    public class AccountDTO : AddressDigitalShortDTO
    {
        private int accountId;

        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; OnPropertyChanged("AccountId"); }
        }
        private int accountTypeId;

        public int AccountTypeId
        {
            get { return accountTypeId; }
            set { accountTypeId = value; OnPropertyChanged("AccountTypeId"); }
        }
        private string accountTypeName;
        public string AccountTypeName
        {
            get { return accountTypeName; }
            set { accountTypeName = value; OnPropertyChanged(nameof(AccountTypeName)); }
        }

        private int employeeId;

        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; OnPropertyChanged("EmployeeId"); }
        }
        private string employeeFullname;

        public string EmployeeFullname
        {
            get { return employeeFullname; }
            set { employeeFullname = value; OnPropertyChanged(nameof(EmployeeFullname)); }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged("Username"); }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(AccountId),AccountId);
            Validation.AddNonNullFields(nameof(AccountTypeId), AccountTypeId);
            Validation.AddNonNullFields(nameof(Username), Username);
            Validation.AddNonNullFields(nameof(Password), Password);
            Validation.AddNonNullFields(nameof(EmailAddress), EmailAddress);
        }
        public AccountDTO()
        {

        }
    }
}

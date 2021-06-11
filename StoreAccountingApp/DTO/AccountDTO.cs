using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private AccountTypeDTO accountTypeDTO;

        public AccountTypeDTO AccountTypeDTO
        {
            get { return accountTypeDTO; }
            set { accountTypeDTO = value; OnPropertyChanged(nameof(AccountTypeDTO)); }
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
            set 
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var pbkdf2 = new Rfc2898DeriveBytes(value, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                password = Convert.ToBase64String(hashBytes);

                OnPropertyChanged("Password");

            }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(AccountId),AccountId);
            Validation.AddNonNullFields(nameof(AccountTypeId), AccountTypeId);
            Validation.AddNonNullFields(nameof(Username), Username);
            Validation.AddNonNullFields(nameof(Password), Password);
            Validation.AddNonNullFields(nameof(EmailAddress), EmailAddress);
            Validation.AddUniqueValueFields(nameof(Username), Username);
            Validation.AddUniqueValueFields(nameof(EmailAddress), EmailAddress);
        }
        public AccountDTO()
        {

        }
    }
}

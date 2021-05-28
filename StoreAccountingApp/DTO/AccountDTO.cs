﻿using System;
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
        private int employeeId;

        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; OnPropertyChanged("EmployeeId"); }
        }
        public virtual Employee Employee { get; set; }
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

    }
}
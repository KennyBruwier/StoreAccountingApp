﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.CustomMethods;

namespace StoreAccountingApp.DTO
{
    public class AccountTypeDTO : BaseDTO
    {
        private int accountTypeId;
        public int AccountTypeId 
        {
            get { return accountTypeId; }
            set { accountTypeId = value; OnPropertyChanged("AccountTypeId"); }
        }
        private string name;
        public string Name 
        {
            get { return name; }
            set 
            {
                name = value; 
                OnPropertyChanged("Name");
            }
        }
        private string description;
        public string Description 
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        public AccountTypeDTO(int accountTypeId, string name, string description)
        {
            AccountTypeId = accountTypeId;
            Name = name;
            Description = description;
        }
        public AccountTypeDTO()
        {
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(AccountTypeId), AccountTypeId);
            Validation.AddNonNullFields(nameof(Name), Name);
        }
    }
}

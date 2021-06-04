using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Services;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using System.Windows.Input;
using System.Data.Entity.Validation;
using System.Diagnostics;
using StoreAccountingApp.Models;
using StoreAccountingApp.GeneralClasses;

namespace StoreAccountingApp.ViewModels
{
    public class DBAccountTypeViewModel : DBViewModelBase<AccountTypeDTO,AccountTypeService,AccountType>
    {
        public DBAccountTypeViewModel()
        {
        }
    }
}

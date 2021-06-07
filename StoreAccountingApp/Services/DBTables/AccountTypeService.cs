using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StoreAccountingApp.CustomMethods;
using System.Data.Entity.Validation;
using StoreAccountingApp.Services.DBTables;

namespace StoreAccountingApp.Services
{
    public class AccountTypeService : BaseService<AccountTypeDTO,AccountType>
    {
        public AccountTypeService()
        {
        }
    }
}

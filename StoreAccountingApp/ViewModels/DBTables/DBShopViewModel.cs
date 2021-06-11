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
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Models;
using StoreAccountingApp.Stores;

namespace StoreAccountingApp.ViewModels
{
    public class DBShopViewModel : DBViewModelBase<ShopDTO,ShopService,Shop>
    {
        public DBShopViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

        }
    }
}

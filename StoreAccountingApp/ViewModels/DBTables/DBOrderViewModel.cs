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

namespace StoreAccountingApp.ViewModels
{
    public class DBOrderViewModel : DBViewModelBase<OrderDTO,OrderService,Order>
    {
        public DBOrderViewModel()
        {
        }
    }
}

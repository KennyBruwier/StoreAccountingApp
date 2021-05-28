using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        private ICommand NavigateHomeCommand { get; }
        public OrdersViewModel(INavigationService<HomeViewModel> navigateHomeCommand)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigateHomeCommand);
        }
    }
}

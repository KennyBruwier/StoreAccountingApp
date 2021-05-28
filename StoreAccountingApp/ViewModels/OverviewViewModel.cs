using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private ICommand NavigateHomeCommand { get; }
        public OverviewViewModel(AccountStore accountStore, INavigationService<HomeViewModel> navigateHomeCommand)
        {
            _accountStore = accountStore;
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigateHomeCommand);
        }

        private void OnCurrentAccountChanged()
        {
            //OnPropertyChanged(nameof(Email));
            //OnPropertyChanged(nameof(Username));
            //OnPropertyChanged(nameof(AccountType));
        }
    }
}

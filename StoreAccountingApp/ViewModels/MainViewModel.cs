using StoreAccountingApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private AccountStore _accountStore;
        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool NotLoggedIn => !_accountStore.IsLoggedIn;
        public string CurrentUser => _accountStore.CurrentAccount?.Username;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public MainViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _accountStore = accountStore;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

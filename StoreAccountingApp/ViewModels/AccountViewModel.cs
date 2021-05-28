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
    public class AccountViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        public string Username => _accountStore.CurrentAccount?.Username;
        public string Email => _accountStore.CurrentAccount?.EmailAddress;
        public string AccountType => _accountStore.CurrentAccount?.AccountType.Name;
        public ICommand NavigateHomeCommand { get; }
        public AccountViewModel(AccountStore accountStore, INavigationService<HomeViewModel> homeNavigationService)
        {
            _accountStore = accountStore;
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }
        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(AccountType));
        }
        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose();
        }
    }
}

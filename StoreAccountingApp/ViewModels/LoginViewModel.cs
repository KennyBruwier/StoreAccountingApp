using StoreAccountingApp.Commands;
using StoreAccountingApp.Models;
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
    public class LoginViewModel : ViewModelBase
    {
        private string _username = "kenny";
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _password = "123";
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand LoginCommand { get; }
        public LoginViewModel(AccountStore accountStore, INavigationService<AccountViewModel> accountNavigationService)
        {
            //NavigationService<AccountViewModel> navigationService = new NavigationService<AccountViewModel>(
            //    navigationStore, () => new AccountViewModel(accountStore, navigationStore));
            LoginCommand = new LoginCommand(this, accountStore, accountNavigationService);
        }
    }
}

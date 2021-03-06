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
    public class HomeViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool NotLoggedIn => !_accountStore.IsLoggedIn;
        public string WelcomeMessage => "Welcome to my accounting application";
        //public ICommand NavigateLoginCommand { get; }
        //public HomeViewModel(INavigationService loginNavigationService)
        //{
        //    NavigateLoginCommand = new NavigateCommand(loginNavigationService);
        //}
        //public NavigationBarViewModel NavigationBarViewModel { get; }
        public ICommand NavigateLoginCommand { get; }
        public HomeViewModel(INavigationService<LoginViewModel> loginNavigationService, AccountStore accountStore = null)
        {
            //NavigateLoginCommand = new NavigateCommand<LoginViewModel>(new NavigationService<LoginViewModel>(
            //    navigationStore,()=>new LoginViewModel(accountStore,navigationStore)));
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
            _accountStore = accountStore;
        }
        //public HomeViewModel(NavigationStore navigationStore)
        //{
        //    //NavigateAccountCommand = new NavigateCommand<AccountViewModel>(navigationStore, () => new AccountViewModel(navigationStore));
        //    NavigateAccountCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));
        //}
    }
}

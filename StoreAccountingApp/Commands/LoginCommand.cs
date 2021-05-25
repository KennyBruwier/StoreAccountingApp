using StoreAccountingApp.Models;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly AccountStore _accountStore;
        private readonly NavigationService<AccountViewModel> _navigationService;

        public LoginCommand(LoginViewModel viewModel, AccountStore accountStore, NavigationService<AccountViewModel> navigationService)
        {
            _navigationService = navigationService;
            _accountStore = accountStore;
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            AccountService accountService = new AccountService();
            Account account = accountService.LoggedIn(_viewModel.Username, _viewModel.Password);
            if (account != null)
            {
                //Account account = new Account()
                //{
                //    EmailAddress = $"{_viewModel.Username}@test.com",
                //    Username = _viewModel.Username
                //};
                _accountStore.CurrentAccount = account;
                _navigationService.Navigate();
            }
            else
            {
                MessageBox.Show("Login failed");
            }
        }
    }
}

using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace StoreAccountingApp.Commands
{
    public class LogoutCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private NavigationStore _navigationStore;
        private readonly INavigationService<TViewModel> _navigationService;

        public LogoutCommand(AccountStore accountStore, INavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
            _accountStore = accountStore;
        }

        public override void Execute(object parameter)
        {
            _accountStore.Logout();
            _navigationService.Navigate();
        }
        //private INavigationService<HomeViewModel> CreateHomeNavigationService()
        //{
        //    return new LayoutNavigationService<HomeViewModel>(
        //        _navigationStore,
        //        () => new HomeViewModel(CreateLoginNavigationService()),
        //        _navigationBarViewModel);
        //}

    }
}

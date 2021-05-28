using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateAccountCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateDataCommand { get; }
        public ICommand NavigateOverviewCommand { get; }
        public ICommand NavigateOrdersCommand { get; }
        public ICommand NavigateUsersListingCommand { get; }
        public ICommand LogoutCommand { get; }

        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool NotLoggedIn => !_accountStore.IsLoggedIn;
        public string CurrentUser => _accountStore.CurrentAccount?.Username;

        public NavigationBarViewModel(
            AccountStore accountStore,
            INavigationService<HomeViewModel>homeNavigationService,
            INavigationService<AccountViewModel> accountNavigationService,
            INavigationService<LoginViewModel> loginNavigationService,
            INavigationService<DataViewModel> dataNavigationService,
            INavigationService<OverviewViewModel> overviewNavigationService,
            INavigationService<OrdersViewModel> ordersNavigationService,
            INavigationService<UsersListingViewModel> usersListingService
            )
        {
            _accountStore = accountStore;
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
            NavigateAccountCommand = new NavigateCommand<AccountViewModel>(accountNavigationService);
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
            NavigateDataCommand = new NavigateCommand<DataViewModel>(dataNavigationService);
            NavigateOverviewCommand = new NavigateCommand<OverviewViewModel>(overviewNavigationService);
            NavigateOrdersCommand = new NavigateCommand<OrdersViewModel>(ordersNavigationService);
            NavigateUsersListingCommand = new NavigateCommand<UsersListingViewModel>(usersListingService);
            LogoutCommand = new LogoutCommand(_accountStore);

            //_accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

            base.Dispose();
        }
    }
}

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
        private NavigationStore _navigationStore;
        private UsersStore _usersStore;
        private NavigationBarViewModel _navigationBarViewModel;
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
            NavigationStore navigationStore,
            UsersStore usersStore
            )
        {
            _accountStore = accountStore;
            _navigationStore = navigationStore;
            _usersStore = usersStore;
            _navigationBarViewModel = this;
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(CreateHomeNavigationService());
            NavigateDataCommand = new NavigateCommand<DataViewModel>(CreateDataNavigationService());
            NavigateAccountCommand = new NavigateCommand<AccountViewModel>(CreateAccountNavigationService());
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(CreateLoginNavigationService());
            NavigateOverviewCommand = new NavigateCommand<OverviewViewModel>(CreateOverviewNavigationService());
            NavigateOrdersCommand = new NavigateCommand<OrdersViewModel>(CreateOrdersNavigationService());
            NavigateUsersListingCommand = new NavigateCommand<UsersListingViewModel>(CreateUsersListingNavigationService());
            LogoutCommand = new LogoutCommand(_accountStore);
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }
        //public NavigationBarViewModel(AccountStore accountStore)
        //{
        //    _accountStore = accountStore;
        //}
        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            //OnPropertyChanged(nameof(_accountStore.CurrentAccount));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

            base.Dispose();
        }
        private INavigationService<HomeViewModel> CreateHomeNavigationService()
        {
            return new LayoutNavigationService<HomeViewModel>(
                _navigationStore,
                () => new HomeViewModel(CreateLoginNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<DataViewModel> CreateDataNavigationService()
        {
            return new LayoutNavigationService<DataViewModel>(
                _navigationStore,
                () => new DataViewModel(_accountStore, new NavigationStore(), CreateHomeNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<OverviewViewModel> CreateOverviewNavigationService()
        {
            return new LayoutNavigationService<OverviewViewModel>(
                _navigationStore,
                () => new OverviewViewModel(_accountStore, CreateHomeNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<OrdersViewModel> CreateOrdersNavigationService()
        {
            return new LayoutNavigationService<OrdersViewModel>(
                _navigationStore,
                () => new OrdersViewModel(CreateHomeNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<LoginViewModel> CreateLoginNavigationService()
        {
            return new LayoutNavigationService<LoginViewModel>(
                _navigationStore,
                () => new LoginViewModel(_accountStore, CreateAccountNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<AccountViewModel> CreateAccountNavigationService()
        {
            return new LayoutNavigationService<AccountViewModel>(
                _navigationStore,
                () => new AccountViewModel(_accountStore, CreateHomeNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<UsersListingViewModel> CreateUsersListingNavigationService()
        {
            return new LayoutNavigationService<UsersListingViewModel>(
                _navigationStore,
                () => new UsersListingViewModel(new UsersStore(), CreateAddUsersNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<AddUserViewModel> CreateAddUsersNavigationService()
        {
            return new LayoutNavigationService<AddUserViewModel>(
                _navigationStore,
                () => new AddUserViewModel(_usersStore, CreateUsersListingNavigationService()),
                _navigationBarViewModel);
        }

    }
}

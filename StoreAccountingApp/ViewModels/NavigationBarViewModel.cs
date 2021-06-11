using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels;
using StoreAccountingApp.ViewModels.Overviews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private AccountStore _accountStore;
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
        //private ICommand logoutCommand;

        //public ICommand LogoutCommand
        //{
        //    get 
        //    { 
        //        return logoutCommand;
        //    }
        //    set { logoutCommand = value; }
        //}

        public ICommand LogoutCommand { get; }
        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool NotLoggedIn => !_accountStore.IsLoggedIn;
        public bool IsAdmin => CheckRole("admin");
        public bool IsStockManager => CheckRole("stock manager");
        public bool IsSeller => CheckRole("seller");
        public string CurrentUser => _accountStore.CurrentAccount?.Username;
        public string CurrentRoles => CurrentRolesAsText();
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
            LogoutCommand = new LogoutCommand<HomeViewModel>(_accountStore, CreateHomeNavigationService());
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }
        private bool CheckRole(string roleName)
        {
            if (_accountStore.CurrentAccount != null)
                switch (roleName.ToLower())
                {
                    case "admin": return _accountStore.CurrentAccount.AccountType.Admin;
                    case "stock manager":return _accountStore.CurrentAccount.AccountType.StockManager;
                    case "seller":return _accountStore.CurrentAccount.AccountType.Seller;
                }
            return false;
        }
        private string CurrentRolesAsText()
        {
            List<string> roles = new List<string>();
            if (_accountStore.CurrentAccount != null)
            {
                if (_accountStore.CurrentAccount.AccountType.Admin) roles.Add("admin");
                if (_accountStore.CurrentAccount.AccountType.Seller) roles.Add("seller");
                if (_accountStore.CurrentAccount.AccountType.StockManager) roles.Add("stock manager");
            }
            if (roles.Count > 0)
                return String.Format("({0})",String.Join(", ", roles.ToArray()));
            else
                return String.Empty;
        }
        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(NotLoggedIn));
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsSeller));
            OnPropertyChanged(nameof(IsStockManager));
            OnPropertyChanged(nameof(CurrentRoles));
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
                () => new OverviewViewModel(_accountStore, new NavigationStore() { CurrentViewModel = new SalesOverviewViewModel() }),
                _navigationBarViewModel);
        }
        private INavigationService<OrdersViewModel> CreateOrdersNavigationService()
        {
            return new LayoutNavigationService<OrdersViewModel>(
                _navigationStore,
                () => new OrdersViewModel(new NavigationStore(),_accountStore),
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

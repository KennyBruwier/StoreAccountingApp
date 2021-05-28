using StoreAccountingApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StoreAccountingApp.ViewModels;
using StoreAccountingApp.Stores;
using StoreAccountingApp.Services;

namespace StoreAccountingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AccountStore _accountStore;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationBarViewModel _navigationBarViewModel;
        public ViewModelBase CurrentViewModel { get; }
        public MainWindow()
        {
            _navigationStore = new NavigationStore();
            _accountStore = new AccountStore();
            _navigationBarViewModel = new NavigationBarViewModel(
                _accountStore,
                CreateHomeNavigationService(),
                CreateAccountNavigationService(),
                CreateLoginNavigationService(),
                CreateDataNavigationService(),
                CreateOverviewNavigationService(),
                CreateOrdersNavigationService(),
                CreateUsersListingNavigationService()
                );
            INavigationService<HomeViewModel> homeNavigationService = CreateHomeNavigationService();
            homeNavigationService.Navigate();
            //_navigationStore.CurrentViewModel = new HomeViewModel(_navigationBarViewModel,_accountStore, _navigationStore);
            DataContext = new MainViewModel(_navigationStore);
            InitializeComponent();
            InitializeDB();
        }
        private INavigationService<DataViewModel> CreateDataNavigationService()
        {
            return new LayoutNavigationService<DataViewModel>(
                _navigationStore,
                () => new DataViewModel(CreateHomeNavigationService()),
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
                () => new LoginViewModel(_accountStore,CreateAccountNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<AccountViewModel> CreateAccountNavigationService()
        {
            return new LayoutNavigationService<AccountViewModel>(
                _navigationStore, 
                () => new AccountViewModel(_accountStore, CreateHomeNavigationService()),
                _navigationBarViewModel);
        }
        private INavigationService<HomeViewModel> CreateHomeNavigationService()
        {
            return new LayoutNavigationService<HomeViewModel>(
                _navigationStore, 
                () => new HomeViewModel(CreateLoginNavigationService()),
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
                () => new AddUserViewModel(new UsersStore(), CreateUsersListingNavigationService()),
                _navigationBarViewModel);
        }
        private void InitializeDB()
        {
            Debug.WriteLine("Starting DB...");
            _DBStoreAccountingContext ctx = new _DBStoreAccountingContext();
            ctx.LoadDemoData();
            Debug.WriteLine("Db successfully initialized");
        }

    }
}

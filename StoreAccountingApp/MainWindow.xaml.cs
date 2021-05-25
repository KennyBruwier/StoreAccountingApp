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
        //AccountTypeViewModel ViewModel;
        //private readonly IServiceProvider _serviceProvider;
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
                CreateLoginNavigationService()
                );
            NavigationService<HomeViewModel> homeNavigationService = CreateHomeNavigationService();
            homeNavigationService.Navigate();
            //_navigationStore.CurrentViewModel = new HomeViewModel(_navigationBarViewModel,_accountStore, _navigationStore);
            DataContext = new MainViewModel(_navigationStore);
            InitializeComponent();
            InitializeDB();
            //CurrentViewModel = new AccountTypeViewModel();
            //this.DataContext = CurrentViewModel;
        }

        private NavigationService<LoginViewModel> CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(
                _navigationStore, () => new LoginViewModel(_accountStore,CreateAccountNavigationService()));
        }

        private NavigationService<AccountViewModel> CreateAccountNavigationService()
        {
            return new NavigationService<AccountViewModel>(
                _navigationStore, () => new AccountViewModel(_navigationBarViewModel, _accountStore, CreateHomeNavigationService()));
        }

        private NavigationService<HomeViewModel> CreateHomeNavigationService()
        {
            return new NavigationService<HomeViewModel>(
                _navigationStore, () => new HomeViewModel(_navigationBarViewModel,CreateLoginNavigationService()));
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

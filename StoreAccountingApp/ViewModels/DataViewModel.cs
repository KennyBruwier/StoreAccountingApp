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
    public class DataViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        //private NavigationStore _navigationStore;
        private readonly NavigationStore _tableNavigationStore;
        public ViewModelBase DBContentViewModel => _tableNavigationStore.CurrentViewModel;
        public ICommand NavigateAccountTypeCommand { get; }
        public ICommand NavigateClientCommand { get; }
        public ICommand NavigateCountryCommand { get; }
        public ICommand NavigateUserCommand { get; }
        public ICommand NavigateDistrictCommand { get; }
        public ICommand NavigateEmployeeCommand { get; }
        public ICommand NavigateJobFunctionCommand { get; }
        public ICommand NavigateOrderProductCommand { get; }
        public ICommand NavigateOrderCommand { get; }
        public ICommand NavigateProductCommand { get; }
        public ICommand NavigateSaleProductCommand { get; }
        public ICommand NavigateSaleCommand { get; }
        public ICommand NavigateStockCommand { get; } 
        public ICommand NavigateShopCommand { get; }
        public ICommand NavigateSupplierProductCommand { get; }
        public ICommand NavigateSupplierCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        public bool IsAdmin => CheckRole("admin");
        public bool IsStockManager => CheckRole("stock manager");
        public bool IsSeller => CheckRole("seller");

        public DataViewModel(AccountStore accountStore, NavigationStore tableNavigationStore, INavigationService<HomeViewModel> navigateHomeCommand)
        {
            _accountStore = accountStore;
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigateHomeCommand);
            if (tableNavigationStore != null)
            {
                _tableNavigationStore = tableNavigationStore;
                _tableNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            }
            NavigateAccountTypeCommand = new NavigateCommand<DBAccountTypeViewModel>(CreateAccountTypeNavigationService());
            NavigateClientCommand = new NavigateCommand<DBClientViewModel>(CreateClientNavigationService());
            NavigateCountryCommand = new NavigateCommand<DBCountryViewModel>(CreateCountryNavigationService());
            NavigateDistrictCommand = new NavigateCommand<DBDistrictViewModel>(CreateDistrictNavigationService());
            NavigateEmployeeCommand = new NavigateCommand<DBEmployeeViewModel>(CreateEmployeeNavigationService());
            NavigateJobFunctionCommand = new NavigateCommand<DBJobFunctionViewModel>(CreateJobFunctionNavigationService());
            NavigateOrderCommand = new NavigateCommand<DBOrderViewModel>(CreateOrderNavigationService());
            NavigateOrderProductCommand = new NavigateCommand<DBOrderProductViewModel>(CreateOrderProductNavigationService());
            NavigateProductCommand = new NavigateCommand<DBProductViewModel>(CreateProductNavigationService());
            NavigateSaleCommand = new NavigateCommand<DBSaleViewModel>(CreateSaleNavigationService());
            NavigateSaleProductCommand = new NavigateCommand<DBSaleProductViewModel>(CreateSaleProductNavigationService());
            NavigateStockCommand = new NavigateCommand<DBStockViewModel>(CreateStockNavigationService());
            NavigateShopCommand = new NavigateCommand<DBShopViewModel>(CreateShopNavigationService());
            NavigateSupplierCommand = new NavigateCommand<DBSupplierViewModel>(CreateSupplierNavigationService());
            NavigateSupplierProductCommand = new NavigateCommand<DBSupplierProductViewModel>(CreateSupplierProductNavigationService());
            NavigateUserCommand = new NavigateCommand<DBAccountViewModel>(CreateUserNavigationService());
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }
        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsSeller));
            OnPropertyChanged(nameof(IsStockManager));
        }
        private bool CheckRole(string roleName)
        {
            if (_accountStore.CurrentAccount != null)
                switch (roleName.ToLower())
                {
                    case "admin": return _accountStore.CurrentAccount.AccountType.Admin;
                    case "stock manager": return _accountStore.CurrentAccount.AccountType.StockManager;
                    case "seller": return _accountStore.CurrentAccount.AccountType.Seller;
                }
            return false;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(DBContentViewModel));
        }
        public override void Dispose()
        {
            DBContentViewModel?.Dispose();
            base.Dispose();
        }
        //private INavigationService<LoginViewModel> CreateLoginNavigationService()
        //{
        //    return new LayoutNavigationService<LoginViewModel>(
        //        _navigationStore,
        //        () => new LoginViewModel(_accountStore, CreateAccountNavigationService()),
        //        _navigationBarViewModel);
        //}
        private INavigationService<DBAccountTypeViewModel> CreateAccountTypeNavigationService()
        {
            return new NavigationService<DBAccountTypeViewModel>(
                _tableNavigationStore,
                () => new DBAccountTypeViewModel());
        }
        private INavigationService<DBClientViewModel> CreateClientNavigationService()
        {
            return new NavigationService<DBClientViewModel>(
                _tableNavigationStore,
                () => new DBClientViewModel());
        }
        private INavigationService<DBCountryViewModel> CreateCountryNavigationService()
        {
            return new NavigationService<DBCountryViewModel>(
                _tableNavigationStore,
                () => new DBCountryViewModel());
        }
        private INavigationService<DBDistrictViewModel> CreateDistrictNavigationService()
        {
            return new NavigationService<DBDistrictViewModel>(
                _tableNavigationStore,
                () => new DBDistrictViewModel());
        }
        private INavigationService<DBEmployeeViewModel> CreateEmployeeNavigationService()
        {
            return new NavigationService<DBEmployeeViewModel>(
                _tableNavigationStore,
                () => new DBEmployeeViewModel());
        }
        private INavigationService<DBJobFunctionViewModel> CreateJobFunctionNavigationService()
        {
            return new NavigationService<DBJobFunctionViewModel>(
                _tableNavigationStore,
                () => new DBJobFunctionViewModel());
        }
        private INavigationService<DBOrderProductViewModel> CreateOrderProductNavigationService()
        {
            return new NavigationService<DBOrderProductViewModel>(
                _tableNavigationStore,
                () => new DBOrderProductViewModel());
        }
        private INavigationService<DBOrderViewModel> CreateOrderNavigationService()
        {
            return new NavigationService<DBOrderViewModel>(
                _tableNavigationStore,
                () => new DBOrderViewModel());
        }
        private INavigationService<DBProductViewModel> CreateProductNavigationService()
        {
            return new NavigationService<DBProductViewModel>(
                _tableNavigationStore,
                () => new DBProductViewModel());
        }
        private INavigationService<DBSaleProductViewModel> CreateSaleProductNavigationService()
        {
            return new NavigationService<DBSaleProductViewModel>(
                _tableNavigationStore,
                () => new DBSaleProductViewModel());
        }
        private INavigationService<DBSaleViewModel> CreateSaleNavigationService()
        {
            return new NavigationService<DBSaleViewModel>(
                _tableNavigationStore,
                () => new DBSaleViewModel());
        }
        private INavigationService<DBStockViewModel> CreateStockNavigationService()
        {
            return new NavigationService<DBStockViewModel>(
                _tableNavigationStore,
                () => new DBStockViewModel());
        }
        private INavigationService<DBShopViewModel> CreateShopNavigationService()
        {
            return new NavigationService<DBShopViewModel>(
                _tableNavigationStore,
                () => new DBShopViewModel());
        }
        private INavigationService<DBSupplierProductViewModel> CreateSupplierProductNavigationService()
        {
            return new NavigationService<DBSupplierProductViewModel>(
                _tableNavigationStore,
                () => new DBSupplierProductViewModel());
        }
        private INavigationService<DBSupplierViewModel> CreateSupplierNavigationService()
        {
            return new NavigationService<DBSupplierViewModel>(
                _tableNavigationStore,
                () => new DBSupplierViewModel());
        }
        private INavigationService<DBAccountViewModel> CreateUserNavigationService()
        {
            return new NavigationService<DBAccountViewModel>(
                _tableNavigationStore,
                () => new DBAccountViewModel());
        }
    }   
}

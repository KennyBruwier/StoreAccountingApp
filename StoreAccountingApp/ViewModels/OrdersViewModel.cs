using StoreAccountingApp.Commands;
using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DTO;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        private readonly NavigationStore _orderNavigationStore;
        private AccountStore _accountStore;
        public bool IsAdmin => CheckRole("admin");
        public bool IsStockManager => CheckRole("stock manager");
        public bool IsSeller => CheckRole("seller");

        public ViewModelBase OrderContentViewModel => _orderNavigationStore.CurrentViewModel;
        public ICommand NavigateSalesOrderCommand { get; }
        public ICommand NavigateOrdersOrderCommand { get; }

        public OrdersViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            _accountStore = accountStore;
            _orderNavigationStore = navigationStore;
            _orderNavigationStore.CurrentViewModelChanged += OnCurrentOverviewChanged;

            NavigateSalesOrderCommand = new NavigateCommand<SalesOrderViewModel>(CreateSalesOrderNavigationService());
            NavigateOrdersOrderCommand = new NavigateCommand<OrdersOrderViewModel>(CreateOrdersOrderNavigationService());
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

        private void OnCurrentOverviewChanged()
        {
            OnPropertyChanged(nameof(OrderContentViewModel));
        }
        private INavigationService<SalesOrderViewModel> CreateSalesOrderNavigationService()
        {
            return new NavigationService<SalesOrderViewModel>(
                _orderNavigationStore,
                () => new SalesOrderViewModel());
        }
        private INavigationService<OrdersOrderViewModel> CreateOrdersOrderNavigationService()
        {
            return new NavigationService<OrdersOrderViewModel>(
                _orderNavigationStore,
                () => new OrdersOrderViewModel());
        }

    }
}

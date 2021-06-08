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
        public ViewModelBase OrderContentViewModel => _orderNavigationStore.CurrentViewModel;
        public ICommand NavigateSalesOrderCommand { get; }
        public ICommand NavigateOrdersOrderCommand { get; }

        public OrdersViewModel(NavigationStore navigationStore)
        {
            _orderNavigationStore = navigationStore;
            _orderNavigationStore.CurrentViewModelChanged += OnCurrentOverviewChanged;

            NavigateSalesOrderCommand = new NavigateCommand<SalesOrderViewModel>(CreateSalesOrderNavigationService());
            NavigateOrdersOrderCommand = new NavigateCommand<OrdersOrderViewModel>(CreateOrdersOrderNavigationService());

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

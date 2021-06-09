using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels.Overviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly NavigationStore _overviewNavigationStore;
        public ViewModelBase OverviewContentViewModel => _overviewNavigationStore.CurrentViewModel;
        public bool IsAdmin => CheckRole("admin");
        public bool IsStockManager => CheckRole("stock manager");
        public bool IsSeller => CheckRole("seller");
        public ICommand NavigateSalesOverviewCommand { get; }
        public ICommand NavigateStocksOverviewCommand { get; }
        public OverviewViewModel(AccountStore accountStore, NavigationStore navigationStore)
        {
            _accountStore = accountStore;
            _overviewNavigationStore = navigationStore;
            _overviewNavigationStore.CurrentViewModelChanged += OnCurrentOverviewChanged;
            NavigateSalesOverviewCommand = new NavigateCommand<SalesOverviewViewModel>(CreateSalesOverviewNavigationService());
            NavigateStocksOverviewCommand = new NavigateCommand<StocksOverviewViewModel>(CreateStocksOverviewNavigationService());
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
            OnPropertyChanged(nameof(OverviewContentViewModel));
        }
        private INavigationService<SalesOverviewViewModel> CreateSalesOverviewNavigationService()
        {
            return new NavigationService<SalesOverviewViewModel>(
                _overviewNavigationStore,
                () => new SalesOverviewViewModel());
        }
        private INavigationService<StocksOverviewViewModel> CreateStocksOverviewNavigationService()
        {
            return new NavigationService<StocksOverviewViewModel>(
                _overviewNavigationStore,
                () => new StocksOverviewViewModel());
        }
    }
}

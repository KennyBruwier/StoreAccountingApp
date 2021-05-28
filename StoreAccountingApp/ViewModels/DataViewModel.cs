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
        private NavigationStore _DBNavigationStore;
        public ViewModelBase DBContentViewModel => _DBNavigationStore?.CurrentViewModel;
        private ICommand NavigateAccountTypeCommand { get; }
        private ICommand NavigateClientCommand { get; }
        private ICommand NavigateCountryCommand { get; }
        private ICommand NavigateUserCommand { get; }
        private ICommand NavigateDistrictCommand { get; }
        private ICommand NavigateEmployeeCommand { get; }
        private ICommand NavigateJobFunctionCommand { get; }
        private ICommand NavigateOrderProductCommand { get; }
        private ICommand NavigateOrderCommand { get; }
        private ICommand NavigateProductCommand { get; }
        private ICommand NavigateSaleProductCommand { get; }
        private ICommand NavigateSaleCommand { get; }
        private ICommand NavigateStockCommand { get; }
        private ICommand NavigateStoreCommand { get; }
        private ICommand NavigateSupplierProductCommand { get; }
        private ICommand NavigateSupplierCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        public DataViewModel(INavigationService<HomeViewModel> navigateHomeCommand, NavigationStore dbContentStore = null)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigateHomeCommand);
            if (dbContentStore != null)
            {
                _DBNavigationStore = dbContentStore;
                _DBNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            }

            NavigateAccountTypeCommand = new NavigateCommand<DBAccountTypeViewModel>(CreateAccountTypeNavigationService());
            //NavigateAccountTypeCommand = new NavigateCommand<AccountViewModel>()
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
        private INavigationService<DBAccountTypeViewModel> CreateAccountTypeNavigationService()
        {
            return new NavigationService<DBAccountTypeViewModel>(
                _DBNavigationStore,
                () => new DBAccountTypeViewModel());
        }
    }
}

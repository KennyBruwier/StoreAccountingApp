using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
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

        public DataViewModel(
            INavigationService<HomeViewModel> navigateHomeCommand)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigateHomeCommand);
        }
    }
}

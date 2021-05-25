using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;
        //private readonly NavigationStore _navigationStore;   -----> moved to navigationService
        //private readonly Func<TViewModel> _createViewModel;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }
        //public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        //{
        //    _navigationStore = navigationStore;
        //    _createViewModel = createViewModel;
        //}
        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
            //_navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}

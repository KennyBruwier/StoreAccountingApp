using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Services
{
    public class DBTableNavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly DBTableStore _dbTableStore;
        private readonly Func<TViewModel> _createDBTableViewModel;
        public DBTableNavigationService(DBTableStore dBTableStore, Func<TViewModel> createDBTableViewModel)
        {
            _dbTableStore = dBTableStore;
            _createDBTableViewModel = createDBTableViewModel;
        }
        public void Navigate()
        {
            _dbTableStore.CurrentViewModel = _createDBTableViewModel();
        }
    }
}

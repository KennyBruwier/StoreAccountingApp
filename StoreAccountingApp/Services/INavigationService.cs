using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
    /*
     * private INavigationService<AddUserViewModel> CreateAddUsersNavigationService()
        {
            return new LayoutNavigationService<AddUserViewModel>(
                _navigationStore, 
                () => new AddUserViewModel(_usersStore, CreateUsersListingNavigationService()),
                _navigationBarViewModel);
        }
     */
}

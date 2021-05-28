using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class AddUserViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddUserViewModel(UsersStore peopleStore, INavigationService<UsersListingViewModel> closeNavigationService)
        {
            SubmitCommand = new AddUserCommand(this, peopleStore, closeNavigationService);
            CancelCommand = new NavigateCommand<UsersListingViewModel>(closeNavigationService);
        }
    }
}

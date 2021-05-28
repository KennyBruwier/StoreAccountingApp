using StoreAccountingApp.Commands;
using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace StoreAccountingApp.ViewModels
{
    public class UsersListingViewModel : ViewModelBase
    {
        private readonly UsersStore _usersStore;

        private readonly ObservableCollection<UserViewModel> _users;

        public IEnumerable<UserViewModel> Users => _users;

        public ICommand AddUserCommand { get; }

        public UsersListingViewModel(UsersStore usersStore, INavigationService<AddUserViewModel> addUserNavigationService)
        {
            _usersStore = usersStore;

            AddUserCommand = new NavigateCommand<AddUserViewModel>(addUserNavigationService);
            _users = new ObservableCollection<UserViewModel>();

            _users.Add(new UserViewModel("SingletonSean"));
            _users.Add(new UserViewModel("Mary"));
            _users.Add(new UserViewModel("Joe"));

            _usersStore.UserAdded += OnUserAdded;
        }

        private void OnUserAdded(string name)
        {
            _users.Add(new UserViewModel(name));
        }
    }
}

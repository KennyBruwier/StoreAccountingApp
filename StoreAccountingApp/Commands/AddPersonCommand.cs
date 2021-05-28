using StoreAccountingApp.Services;
using StoreAccountingApp.Stores;
using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreAccountingApp.Commands
{
    public class AddUserCommand : CommandBase
    {
        private readonly AddUserViewModel _addUserViewModel;
        private readonly UsersStore _peopleStore;
        private readonly INavigationService<UsersListingViewModel> _navigationService;

        public AddUserCommand(AddUserViewModel addUserViewModel, UsersStore peopleStore, INavigationService<UsersListingViewModel>navigationService)
        {
            _addUserViewModel = addUserViewModel;
            _peopleStore = peopleStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            string name = _addUserViewModel.Name;
            _peopleStore.AddUser(name);
            _navigationService.Navigate();
        }
    }
}

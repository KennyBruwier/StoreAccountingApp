using System;
using System.Collections.Generic;
using System.Text;

namespace StoreAccountingApp.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public string Name { get; }

        public UserViewModel(string name)
        {
            Name = name;
        }
    }
}

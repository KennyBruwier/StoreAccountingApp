using System;
using System.Collections.Generic;
using System.Text;

namespace StoreAccountingApp.Stores
{
    public class UsersStore
    {
        public event Action<string> UserAdded;

        public void AddUser(string name)
        {
            UserAdded?.Invoke(name);
        }
    }
}

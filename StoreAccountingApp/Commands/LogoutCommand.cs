using StoreAccountingApp.Commands;
using StoreAccountingApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreAccountingApp.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly AccountStore _accountStore;

        public LogoutCommand(AccountStore accountStore)
        {
            _accountStore = accountStore;
        }

        public override void Execute(object parameter)
        {
            _accountStore.Logout();
        }
    }
}

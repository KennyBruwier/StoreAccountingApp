using StoreAccountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.Commands
{
    public class NavigateDBCommand<TViewModel> : CommandBase where TViewModel:ViewModelBase
    {
        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

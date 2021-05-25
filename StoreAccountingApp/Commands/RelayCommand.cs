using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreAccountingApp.Commands
{
    public class RelayCommand : CommandBase
    {
        private Action DoWork;
        public RelayCommand(Action work)
        {
            DoWork = work;
        }
        public override void Execute(object parameter)
        {
            DoWork();
        }
    }
}

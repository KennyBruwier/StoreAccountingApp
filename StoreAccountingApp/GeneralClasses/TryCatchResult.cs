using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.GeneralClasses
{
    public class TryCatchResult
    {
        public bool Result { get; set; }
        public string OperationMessage { get; set; }
        public string ErrMessage { get; set; }
    }
}

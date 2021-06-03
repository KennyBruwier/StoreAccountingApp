using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels.BaseModels
{
    public interface IDBOperation
    {
        void LoadData();
        bool Save();
        bool Update();
        bool Delete();
    }
}

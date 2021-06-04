using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.GeneralClasses
{
    public class DBField
    {
        public DBField(string name, object value)
        {
            Value = value;
            Name = name;
        }
        public DBField()
        {

        }
        public object Value { get; set; }
        public string Name { get; set; }
    }
}

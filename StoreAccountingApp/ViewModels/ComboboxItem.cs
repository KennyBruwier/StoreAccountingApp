using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels
{
    public class ComboboxItem
    {
        public int Key { get; set; }
        public string Text { get; set; } = "";
        public ComboboxItem(int key, string text)
        {
            Key = key;
            Text = text;
        }
        public ComboboxItem()
        {

        }
        public override string ToString()
        {
            return Text;
        }
    }
}

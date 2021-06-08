using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.GeneralClasses
{
    public class OrdersDataGrid
    {
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private int amount;

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        private decimal netto;

        public decimal Netto
        {
            get { return netto; }
            set { netto = value; }
        }
        private decimal vat;

        public decimal VAT
        {
            get { return vat; }
            set { vat = value; }
        }
        private decimal total;

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
        public OrdersDataGrid(object item)
        {
            var itemProperties = item.GetType().GetProperties().Where(x => x.CanRead).ToList();
            var targetProperties = GetType().GetProperties().Where(x => x.CanWrite).ToList();
            foreach (PropertyInfo itemProp in itemProperties)
            {
                foreach (PropertyInfo targetProp in targetProperties)
                {
                    if (itemProp.Name.Equals(targetProp.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        targetProp.SetValue(this, itemProp.GetValue(item, null), null);
                        break;
                    }
                }
            }
        }
        public OrdersDataGrid()
        {

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.DTO.Abstracts
{
    public class ProductRecordDTO : RecordTimeStampsDTO
    {
        private string barcode;

        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; OnPropertyChanged("Barcode"); }
        }
        private int amount;

        public int Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }
        private int minAmount;

        public int MinAmount
        {
            get { return minAmount; }
            set { minAmount = value; OnPropertyChanged("MinAmount"); }
        }
        private int maxAmount;

        public int MaxAmount
        {
            get { return maxAmount; }
            set { maxAmount = value; OnPropertyChanged("MaxAmount"); }
        }
        private double unitPrice;

        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; OnPropertyChanged("UnitPrice"); }
        }
        private float discount;

        public float Discount
        {
            get { return discount; }
            set { discount = value; OnPropertyChanged("Discount"); }
        }
        private int guarantee;

        public int Guarantee
        {
            get { return guarantee; }
            set { guarantee = value; OnPropertyChanged("Guarantee"); }
        }

    }
}

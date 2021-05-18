
namespace StoreAccountingApp.Models.DTO.Abstracts
{
    public abstract class ItemTransactionDTO : RecordTimeStampsDTO
    {
        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        private float unitPrice;
        public float UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; OnPropertyChanged("UnitPrice"); }
        }
        private float vAT;
        public float VAT
        {
            get { return vAT; }
            set { vAT = value; OnPropertyChanged("VAT"); }
        }
        private float discount;
        public float Discount
        {
            get { return discount; }
            set { discount = value; OnPropertyChanged("Discount"); }
        }
        private float guarantee;
        public float Guarantee
        {
            get { return guarantee; }
            set { guarantee = value; OnPropertyChanged("Guarantee"); }
        }
    }
}

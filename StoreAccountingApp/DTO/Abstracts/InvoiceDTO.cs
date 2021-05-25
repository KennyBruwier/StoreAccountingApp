using System;

namespace StoreAccountingApp.DTO.Abstracts
{
    public abstract class InvoiceDTO : RecordTimeStampsDTO
    {
        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; OnPropertyChanged("InvoiceNumber"); }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        private float guarantee;
        public float Guarantee
        {
            get { return guarantee; }
            set { guarantee = value; OnPropertyChanged("Guarantee"); }
        }
        private float discount;
        public float Discount
        {
            get { return discount; }
            set { discount = value; OnPropertyChanged("Discount"); }
        }
        private string paymentMethod;
        public string PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; OnPropertyChanged("PaymentMethod"); }
        }
        private DateTime purchaseDate;
        public DateTime PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; OnPropertyChanged("PurchaseDate"); }
        }
        private DateTime expirationDate;
        public DateTime ExpirationDate
        {
            get { return expirationDate;; }
            set { expirationDate = value; OnPropertyChanged("ExpirationDate"); }
        }
        private DateTime paymentDate;
        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; OnPropertyChanged("PaymentDate"); }
        }
        private DateTime deliveryDate;
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; OnPropertyChanged("Deliverydate"); }
        }
    }
}

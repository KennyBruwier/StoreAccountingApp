using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class StockDTO : RecordTimeStampsDTO
    {
        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; OnPropertyChanged("ShopId"); }
        }
        private ShopDTO shopDTO;

        public ShopDTO ShopDTO
        {
            get { return shopDTO; }
            set { shopDTO = value; OnPropertyChanged(nameof(ShopDTO)); }
        }
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; OnPropertyChanged("ProductId"); }
        }
        private ProductDTO productDTO;

        public ProductDTO ProductDTO
        {
            get { return productDTO; }
            set { productDTO = value; OnPropertyChanged(nameof(ProductDTO)); }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged("Category"); }
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
        private decimal unitPrice;
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; OnPropertyChanged("UnitPrice"); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(ShopId), ShopId);
            Validation.AddPrimaryKey(nameof(ProductId), ProductId);
        }
    }
}

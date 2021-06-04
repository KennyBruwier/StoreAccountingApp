using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class SaleProductDTO : ItemTransactionDTO
    {
        private int saleId;
        public int SaleId
        {
            get { return saleId; }
            set { saleId = value; OnPropertyChanged("SaleId"); }
        }
        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; OnPropertyChanged("ProductId"); }
        }
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(SaleId), SaleId);
            Validation.AddPrimaryKey(nameof(ProductId), ProductId);
            Validation.AddNonNullFields(nameof(Status), Status);
        }
    }
}

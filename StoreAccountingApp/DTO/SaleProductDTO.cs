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
        private SaleDTO saleDTO;

        public SaleDTO SaleDTO
        {
            get { return saleDTO; }
            set { saleDTO = value; OnPropertyChanged(nameof(SaleDTO)); }
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
        public override void LoadValidation()
        {
            Validation = new GeneralClasses.CheckValidation();
            Validation.AddPrimaryKey(nameof(SaleId), SaleId);
            Validation.AddPrimaryKey(nameof(ProductId), ProductId);
            Validation.AddNonNullFields(nameof(Status), Status);
        }
    }
}

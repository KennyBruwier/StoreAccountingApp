using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.DTO.Abstracts;

namespace StoreAccountingApp.DTO
{
    public class SupplierProductDTO : ProductRecordDTO
    {
        private int supplierId;
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }
        private SupplierDTO supplierDTO;

        public SupplierDTO SupplierDTO
        {
            get { return supplierDTO; }
            set { supplierDTO = value; OnPropertyChanged(nameof(SupplierDTO)); }
        }
        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
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
            Validation.AddPrimaryKey(nameof(SupplierId), SupplierId);
            Validation.AddPrimaryKey(nameof(ProductId), ProductId);
        }
    }
}

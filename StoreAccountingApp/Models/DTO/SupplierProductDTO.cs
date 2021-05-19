using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreAccountingApp.Models.DTO.Abstracts;

namespace StoreAccountingApp.Models.DTO
{
    public class SupplierProductDTO : ProductRecordDTO
    {
        private int supplierId;
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }
        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

    }
}

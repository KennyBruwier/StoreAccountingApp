using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Validation;
using StoreAccountingApp.Services.DBTables;

namespace StoreAccountingApp.Services
{
    public class SupplierProductService : BaseService<SupplierProductDTO,SupplierProduct>
    {
        public SupplierProductService()
        {

        }
        public override SupplierProductDTO CopyDBtoDTO(SupplierProduct source)
        {
            SupplierProductDTO supplierProductDTO = ObjMethods.CopyProperties<SupplierProduct, SupplierProductDTO>(source);
            if (supplierProductDTO.SupplierId != 0)
            {
                SupplierService supplierService = new SupplierService();
                supplierProductDTO.SupplierDTO = supplierService.Search(supplierProductDTO.SupplierId);
            }
            if (supplierProductDTO.ProductId != 0)
            {
                ProductService productService = new ProductService();
                supplierProductDTO.ProductDTO = productService.Search(supplierProductDTO.ProductId);
            }
            return supplierProductDTO;
        }
    }
}

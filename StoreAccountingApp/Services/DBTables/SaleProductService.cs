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
    public class SaleProductService : BaseService<SaleProductDTO,SaleProduct>
    {
        public SaleProductService()
        {

        }
        public override SaleProductDTO CopyDBtoDTO(SaleProduct source)
        {
            SaleProductDTO saleProductDTO = ObjMethods.CopyProperties<SaleProduct, SaleProductDTO>(source);
            if (saleProductDTO.ProductId != 0)
            {
                ProductService productService = new ProductService();
                saleProductDTO.ProductDTO = productService.Search(saleProductDTO.ProductId);
            }
            if (saleProductDTO.SaleId != 0)
            {
                SaleService saleService = new SaleService();
                saleProductDTO.SaleDTO = saleService.Search(saleProductDTO.SaleId);
            }
            return saleProductDTO;
        }
    }
}

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
    public class StockService : BaseService<StockDTO,Stock>
    {
        public StockService()
        {

        }
        public override StockDTO CopyDBtoDTO(Stock source)
        {
            StockDTO stockDTO = ObjMethods.CopyProperties<Stock, StockDTO>(source);
            if (stockDTO.ShopId != 0)
            {
                ShopService shopService = new ShopService();
                stockDTO.ShopDTO = shopService.Search(stockDTO.ShopId);
            }
            if (stockDTO.ProductId != 0)
            {
                ProductService productService = new ProductService();
                stockDTO.ProductDTO = productService.Search(stockDTO.ProductId);
            }
            return stockDTO;
        }
    }
}

using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DTO;
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.ViewModels.Orders
{
    public class SalesOrderViewModel : ViewModelBase
    {
        private ProductService _productService;
        private SaleService _saleService;
        private SaleProductService _saleProductService;
        private List<ProductDTO> productsList;

        public List<ProductDTO> ProductsList
        {
            get { return productsList; }
            set { productsList = value; }
        }
        private List<SaleProductDTO> saleProductList;

        public List<SaleProductDTO> SaleProductList
        {
            get { return saleProductList; }
            set { saleProductList = value; }
        }
        private List<SaleDTO> salesList;

        public List<SaleDTO> SalesList
        {
            get { return salesList; }
            set { salesList = value; }
        }
        private List<ComboboxItem> cbSalesList;

        public List<ComboboxItem> CbSalesList
        {
            get { return cbSalesList; }
            set { cbSalesList = value; }
        }
        private List<OrdersDataGrid> dgList;

        public List<OrdersDataGrid> DgList
        {
            get { return dgList; }
            set { dgList = value; }
        }

        public SalesOrderViewModel()
        {
            _productService = new ProductService();
            _saleProductService = new SaleProductService();
            _saleService = new SaleService();
            SaleProductList = _saleProductService.GetAll();
            SalesList = _saleService.GetAll();
            ProductsList = _productService.GetAll().Where(p => SaleProductList.Select(sp => sp.ProductId).Contains(p.ProductId)).ToList(); ;
            CbSalesList = ObjMethods.CreateComboboxList<SaleDTO, ComboboxItem>(SalesList, "SaleId", "InvoiceNumber");
            DgList = CreateDataGridListFromSaleProducts();
        }
        public List<OrdersDataGrid> CreateDataGridListFromSaleProducts()
        {
            OrdersDataGrid grandTotal = null;
            List<OrdersDataGrid> newDataGridList = new List<OrdersDataGrid>();
            var dDgList = SaleProductList.Join(ProductsList,
                op => op.ProductId,
                p => p.ProductId,
                (op, p) => new
                {
                    op,
                    p
                })
                .GroupBy(p => p.p.Name)
                .Select(g => new
                {
                    Key = g.Key,
                    Amount = g.Sum(s => s.op.Amount),
                    Netto = g.Sum(s => s.op.UnitPrice),
                    VAT = g.Sum(s => s.op.VAT),
                    Total = g.Sum(s => (s.op.UnitPrice + s.op.VAT))
                })
                .OrderByDescending(g => g.Total).ToList();
            foreach (var item in dDgList)
            {
                OrdersDataGrid dgItem = new OrdersDataGrid(item);
                if (grandTotal == null) grandTotal = new OrdersDataGrid() { Key = "Total: " };
                grandTotal.Total += dgItem.Total;
                grandTotal.Netto += dgItem.Netto;
                grandTotal.VAT += dgItem.VAT;
                grandTotal.Amount += dgItem.Amount;
                newDataGridList.Add(dgItem);
            }
            if (grandTotal != null)
                newDataGridList.Add(grandTotal);
            return newDataGridList;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StoreAccountingApp.Services;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO;
using System.Windows.Input;
using System.Data.Entity.Validation;
using StoreAccountingApp.GeneralClasses;
using StoreAccountingApp.Models;
using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Stores;

namespace StoreAccountingApp.ViewModels
{
    public class DBStockViewModel : DBViewModelBase<StockDTO,StockService,Stock>
    {
        private readonly ShopService _ShopService;
        private readonly ProductService _ProductService;
        public DBStockViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

            _ProductService = new ProductService();
            _ShopService = new ShopService();
            LoadData();
        }
        #region DisplayOperation
        private List<ProductDTO> productList;
        public List<ProductDTO> ProductList
        {
            get { return productList; }
            set { productList = value; OnPropertyChanged("ProductList"); }
        }
        private List<ComboboxItem> cbProductList;
        public List<ComboboxItem> CbProductList
        {
            get { return cbProductList; }
            set { cbProductList = value; OnPropertyChanged("CbProductList"); }
        }
        private List<ShopDTO> shopList;
        public List<ShopDTO> ShopList
        {
            get { return shopList; }
            set { shopList = value; OnPropertyChanged("ShopList"); }
        }
        private List<ComboboxItem> cbShopList;
        public List<ComboboxItem> CbShopList
        {
            get { return cbShopList; }
            set { cbShopList = value; OnPropertyChanged("CbShopList"); }
        }
        private ComboboxItem selectedShop;

        public ComboboxItem SelectedShop
        {
            get { return selectedShop; }
            set
            {
                selectedShop = value;
                OnPropertyChanged(nameof(SelectedShop));
                CurrentDTOModel.ShopId = selectedShop.Key;
            }
        }
        private ComboboxItem selectedProduct;

        public ComboboxItem SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                CurrentDTOModel.ProductId = selectedProduct.Key;
            }
        }

        #endregion
        private void LoadData()
        {
            ProductList = _ProductService.GetAll();
            ShopList = _ShopService.GetAll();
            cbProductList = ObjMethods.CreateComboboxList<ProductDTO, ComboboxItem>(ProductList, "ProductId", "Name", "Manufacturer");
            cbShopList = ObjMethods.CreateComboboxList<ShopDTO, ComboboxItem>(ShopList, "ShopId", "BuildingName", "DistrictName");
        }
    }
}

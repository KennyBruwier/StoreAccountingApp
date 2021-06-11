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
    public class DBSupplierProductViewModel : DBViewModelBase<SupplierProductDTO,SupplierProductService,SupplierProduct>
    {
        private readonly SupplierService _SupplierService;
        private readonly ProductService _ProductService;
        public DBSupplierProductViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

            _ProductService = new ProductService();
            _SupplierService = new SupplierService();
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
        private List<SupplierDTO> supplierList;
        public List<SupplierDTO> SupplierList
        {
            get { return supplierList; }
            set { supplierList = value; OnPropertyChanged("SupplierList"); }
        }
        private List<ComboboxItem> cbSupplierList;
        public List<ComboboxItem> CbSupplierList
        {
            get { return cbSupplierList; }
            set { cbSupplierList = value; OnPropertyChanged("CbSupplierList"); }
        }
        private ComboboxItem selectedSupplier;

        public ComboboxItem SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                selectedSupplier = value;
                OnPropertyChanged(nameof(SelectedSupplier));
                CurrentDTOModel.SupplierId = selectedSupplier.Key;
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
            SupplierList = _SupplierService.GetAll();
            cbProductList = ObjMethods.CreateComboboxList<ProductDTO, ComboboxItem>(ProductList, "ProductId", "Name", "Manufacturer");
            cbSupplierList = ObjMethods.CreateComboboxList<SupplierDTO, ComboboxItem>(SupplierList, "SupplierId", "Firstname", "Lastname","Organisation");
        }
    }
}

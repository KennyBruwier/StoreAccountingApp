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

namespace StoreAccountingApp.ViewModels
{
    public class DBSupplierProductViewModel : DBViewModelBase<SupplierProductDTO,SupplierProductService,SupplierProduct>
    {
        private readonly SupplierService _SupplierService;
        private readonly ProductService _ProductService;
        public DBSupplierProductViewModel()
        {
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

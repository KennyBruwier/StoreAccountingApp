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
    public class DBSaleProductViewModel : DBViewModelBase<SaleProductDTO,SaleProductService,SaleProduct>
    {
        private readonly SaleService _SaleService;
        private readonly ProductService _ProductService;
        private readonly SaleProductService _SaleProductService;
        private readonly EmployeeService _EmployeeService;
        private readonly ClientService _ClientService;
        public DBSaleProductViewModel()
        {
            _ProductService = new ProductService();
            _SaleService = new SaleService();
            _SaleProductService = new SaleProductService();
            _EmployeeService = new EmployeeService();
            _ClientService = new ClientService();
            LoadData();
        }
        #region DisplayOperation
        private List<ClientDTO> clientsList;

        public List<ClientDTO> ClientsList
        {
            get { return clientsList; }
            set { clientsList = value; OnPropertyChanged(nameof(ClientsList)); }
        }

        private List<EmployeeDTO> employeesList;
        public List<EmployeeDTO> EmployeesList
        {
            get { return employeesList; }
            set { employeesList = value;OnPropertyChanged(nameof(EmployeesList)); }
        }
        private List<SaleProductDTO> saleProductList;
        public List<SaleProductDTO> SaleProductList
        {
            get { return saleProductList; }
            set { saleProductList = value; OnPropertyChanged(nameof(SaleProductList)); }
        }
        private List<ProductDTO> productList;
        public List<ProductDTO> ProductList
        {
            get { return productList; }
            set { productList = value; OnPropertyChanged(nameof(ProductList)); }
        }
        private List<SaleDTO> orderList;
        public List<SaleDTO> SaleList
        {
            get { return orderList; }
            set { orderList = value; OnPropertyChanged(nameof(SaleList)); }
        }
        private List<ComboboxItem> cbProductList;
        public List<ComboboxItem> CbProductList
        {
            get { return cbProductList; }
            set { cbProductList = value; OnPropertyChanged(nameof(CbProductList)); }
        }
        private List<ComboboxItem> cbSaleList;
        public List<ComboboxItem> CbSaleList
        {
            get { return cbSaleList; }
            set { cbSaleList = value; OnPropertyChanged(nameof(CbSaleList)); }
        }
        private List<ComboboxItem> cbClientList;
        public List<ComboboxItem> CbClientList
        {
            get { return cbClientList; }
            set { cbClientList = value; OnPropertyChanged(nameof(CbClientList)); }
        }
        private List<ComboboxItem> cbSaleProductList;

        public List<ComboboxItem> CbSaleProductList
        {
            get { return cbSaleProductList; }
            set { cbSaleProductList = value; OnPropertyChanged(nameof(CbSaleProductList)); }
        }
        private List<ComboboxItem> cbEmployeeList;

        public List<ComboboxItem> CbEmployeeList
        {
            get { return cbEmployeeList; }
            set { cbEmployeeList = value; OnPropertyChanged(nameof(CbEmployeeList)); }
        }


        #endregion
        private void LoadData()
        {
            SaleProductList = _SaleProductService.GetAll();
            SaleList = _SaleService.GetAll().Where(s=>SaleProductList.Select(sp=>sp.SaleId).Contains(s.SaleId)).ToList();
            ProductList = _ProductService.GetAll().Where(p=>SaleProductList.Select(sp=>sp.ProductId).Contains(p.ProductId)).ToList();
            EmployeesList = _EmployeeService.GetAll().Where(e => SaleList.Select(s => s.EmployeeId).Contains(e.EmployeeId)).ToList();
            ClientsList = _ClientService.GetAll().Where(c => SaleList.Select(s => s.ClientId).Contains(c.ClientId)).ToList();
            CbProductList = ObjMethods.CreateComboboxList<ProductDTO, ComboboxItem>(ProductList, "ProductId", "Name", "Manufacturer");
            CbSaleList = ObjMethods.CreateComboboxList<SaleDTO, ComboboxItem>(SaleList, "SaleId", "InvoiceNumber", "SupplierName");
            CbEmployeeList = ObjMethods.CreateComboboxList<EmployeeDTO, ComboboxItem>(EmployeesList, "EmployeeId", "Firstname");
            CbClientList = ObjMethods.CreateComboboxList<ClientDTO, ComboboxItem>(ClientsList, "ClientId", "Firstname");
        }

    }
}

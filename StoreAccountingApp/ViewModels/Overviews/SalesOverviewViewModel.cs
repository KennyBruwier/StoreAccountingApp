using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DTO;
using StoreAccountingApp.Models;
using StoreAccountingApp.Services;

namespace StoreAccountingApp.ViewModels.Overviews
{
    public class SalesOverviewViewModel : BaseOverviewViewModel<SaleProductDTO,SaleProductService,SaleProduct>
    {
        public SeriesCollection Collection { get; set; }
        private string xaxisLabel;

        public string XaxisLabel
        {
            get { return xaxisLabel; }
            set { xaxisLabel = value; OnPropertyChanged(nameof(XaxisLabel)); }
        }
        private string yaxisLabel;

        public string YaxisLabel
        {
            get { return yaxisLabel; }
            set { yaxisLabel = value; OnPropertyChanged(nameof(YaxisLabel)); }
        }

        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        private readonly SaleService _SaleService;
        private readonly ProductService _ProductService;
        private readonly SaleProductService _SaleProductService;
        private readonly EmployeeService _EmployeeService;
        private readonly ClientService _ClientService;
        public event Action SelectedItemChanged;

        public SalesOverviewViewModel()
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
            set { employeesList = value; OnPropertyChanged(nameof(EmployeesList)); }
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
        private ComboboxItem cbSelectedProduct ;

        public ComboboxItem CbSelectedProduct
        {
            get 
            { 
                return cbSelectedProduct; 
            }
            set 
            { 
                cbSelectedProduct = value; 
                OnPropertyChanged(nameof(CbSelectedProduct));
                CreateNewSoldProductCollection();
            }
        }
        private ComboboxItem cbSelectedSale;

        public ComboboxItem CbSelectedSale
        {
            get { return cbSelectedSale; }
            set { cbSelectedSale = value; OnPropertyChanged(nameof(CbSelectedSale)); }
        }
        private ComboboxItem cbSelectedClient;

        public ComboboxItem CbSelectedClient
        {
            get { return cbSelectedClient; }
            set { cbSelectedClient = value; OnPropertyChanged(nameof(CbSelectedClient)); }
        }
        private ComboboxItem cbSelectedSaleProduct;

        public ComboboxItem CbSelectedSaleProduct
        {
            get { return cbSelectedSaleProduct; }
            set { cbSelectedSaleProduct = value; OnPropertyChanged(nameof(CbSelectedSaleProduct)); }
        }
        private ComboboxItem cbSelectedEmployee;

        public ComboboxItem CbSelectedEmployee
        {
            get { return cbSelectedEmployee; }
            set { cbSelectedEmployee = value; OnPropertyChanged(nameof(CbSelectedEmployee)); }
        }

        #endregion
        private void LoadData()
        {
            SaleProductList = _SaleProductService.GetAll();
            SaleList = _SaleService.GetAll().Where(s => SaleProductList.Select(sp => sp.SaleId).ToArray().Contains(s.SaleId)).ToList();
            ProductList = _ProductService.GetAll().Where(p => SaleProductList.Select(sp => sp.ProductId).Contains(p.ProductId)).ToList();
            EmployeesList = _EmployeeService.GetAll().Where(e => SaleList.Select(s => s.EmployeeId).Contains(e.EmployeeId)).ToList();
            ClientsList = _ClientService.GetAll().Where(c => SaleList.Select(s => s.ClientId).Contains(c.ClientId)).ToList();
            CbProductList = ObjMethods.CreateComboboxList<ProductDTO, ComboboxItem>(ProductList.Distinct().ToList(), "ProductId", "Name", "Manufacturer");
            CbSaleList = ObjMethods.CreateComboboxList<SaleDTO, ComboboxItem>(SaleList, "SaleId", "InvoiceNumber", "SupplierName");
            CbEmployeeList = ObjMethods.CreateComboboxList<EmployeeDTO, ComboboxItem>(EmployeesList, "EmployeeId", "Firstname");
            CbClientList = ObjMethods.CreateComboboxList<ClientDTO, ComboboxItem>(ClientsList, "ClientId", "Firstname");
            this.SelectedItemChanged += OnSelectedItemChanged;
            CreateNewSoldProductCollection();
        }
        private void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(CbSelectedEmployee));
            OnPropertyChanged(nameof(CbSelectedClient));
            OnPropertyChanged(nameof(CbSelectedProduct));
            OnPropertyChanged(nameof(CbSelectedSale));
            OnPropertyChanged(nameof(CbSelectedSaleProduct));
            OnPropertyChanged(nameof(XaxisLabel));
            OnPropertyChanged(nameof(YaxisLabel));
        }
        private void CreateNewSoldProductCollection()
        {
            var myList = SaleProductList.Join(ProductList,
                        sp => sp.ProductId,
                        p => p.ProductId,
                        (sp, p) => new
                        {
                            sp,
                            p
                        })
                .GroupBy(p => p.p.Name)
                .Select(g => new
                {
                    Key = g.Key,
                    Count = g.Sum(s => s.sp.Amount),
                    Net = g.Sum(s => s.sp.UnitPrice * s.sp.Amount),
                    Gross = g.Sum(s => s.sp.VAT + s.sp.UnitPrice * s.sp.Amount)
                })
                .OrderByDescending(g => g.Count);
            SeriesCollection seriesCollection = new SeriesCollection();
            List<ChartValues<int>> chartvalue = new List<ChartValues<int>>();
            ChartValues<int> listCount = new ChartValues<int>();
            ChartValues<decimal> listNet = new ChartValues<decimal>();
            ChartValues<decimal> listGross = new ChartValues<decimal>();
            List<string> newLabels = new List<string>();
            foreach (var group in myList)
            {
                listCount.Add(group.Count);
                listNet.Add(group.Net);
                listGross.Add(group.Gross);
                newLabels.Add(group.Key);
            }
            seriesCollection.Add(new ColumnSeries
            {
                Title = "Amount",
                Values = listCount
            });//seriesCollection.Add(new Col)
            Collection = seriesCollection;
            Collection.Add(new ColumnSeries
            {
                Title = "Net",
                Values = listNet
            });
            Collection.Add(new ColumnSeries
            {
                Title = "Gross",
                Values = listGross
            });
            //Collection.Add
            Labels = newLabels.ToArray();
            Formatter = value => value.ToString("N");
            YaxisLabel = "Amount sold";
            XaxisLabel = "Product";
        }
        private void LoadDemo()
        {
            Collection = new SeriesCollection()
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = new ChartValues<double>{10,50,39,50}
                }
            };
            Collection.Add(new ColumnSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 10, 56, 42 }
            });
            Collection[1].Values.Add(48d);
            Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");
        }
    }
}

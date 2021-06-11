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
    public class DBSaleViewModel : DBViewModelBase<SaleDTO,SaleService,Sale>
    {
        private ShopService _shopService;
        private EmployeeService _employeeService;
        private ClientService _clientService;

        private List<ComboboxItem> cbShopList;

        public List<ComboboxItem> CbShopList
        {
            get { return cbShopList; }
            set { cbShopList = value; }
        }
        private List<ComboboxItem> cbEmployeeList;

        public List<ComboboxItem> CbEmployeeList
        {
            get { return cbEmployeeList; }
            set { cbEmployeeList = value; }
        }
        private List<ComboboxItem> cbClientList;

        public List<ComboboxItem> CbClientList
        {
            get { return cbClientList; }
            set { cbClientList = value; }
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
        private ComboboxItem selectedEmployee;

        public ComboboxItem SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
                CurrentDTOModel.EmployeeId = selectedEmployee.Key;
            }
        }
        private ComboboxItem selectedSale;
        public ComboboxItem SelectedSale
        {
            get { return selectedSale; }
            set
            {
                selectedSale = value;
                OnPropertyChanged(nameof(SelectedSale));
                CurrentDTOModel.SaleId = selectedSale.Key;
            }
        }
        private ComboboxItem selectedClient;
        public ComboboxItem SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
                CurrentDTOModel.ClientId = selectedClient.Key;
            }
        }

        public DBSaleViewModel(AccountStore accountStore)
        {
            _accountStore = accountStore;
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

            _shopService = new ShopService();
            _employeeService = new EmployeeService();
            _clientService = new ClientService();
            CbShopList = ObjMethods.CreateComboboxList<ShopDTO, ComboboxItem>(_shopService.GetAll(), "ShopId", "BuildingName");
            CbEmployeeList = ObjMethods.CreateComboboxList<EmployeeDTO, ComboboxItem>(_employeeService.GetAll(), "EmployeeId", "Firstname","Lastname");
            CbClientList = ObjMethods.CreateComboboxList<ClientDTO, ComboboxItem>(_clientService.GetAll(), "ClientId", "Firstname", "Lastname");

        }
    }
}

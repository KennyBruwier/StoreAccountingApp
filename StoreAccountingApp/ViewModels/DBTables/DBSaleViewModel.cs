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
        public DBSaleViewModel()
        {
            _shopService = new ShopService();
            _employeeService = new EmployeeService();
            _clientService = new ClientService();
            CbShopList = ObjMethods.CreateComboboxList<ShopDTO, ComboboxItem>(_shopService.GetAll(), "ShopId", "BuildingName");
            CbEmployeeList = ObjMethods.CreateComboboxList<EmployeeDTO, ComboboxItem>(_employeeService.GetAll(), "EmployeeId", "Firstname","Lastname");
            CbClientList = ObjMethods.CreateComboboxList<ClientDTO, ComboboxItem>(_clientService.GetAll(), "ClientId", "Firstname", "Lastname");

        }
    }
}

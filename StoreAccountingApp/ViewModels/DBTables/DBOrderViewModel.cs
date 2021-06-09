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
    public class DBOrderViewModel : DBViewModelBase<OrderDTO,OrderService,Order>
    {
        private ShopService _shopService;
        private EmployeeService _employeeService;
        private SupplierService _supplierService;
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
        private List<ComboboxItem> cbSupplierList;

        public List<ComboboxItem> CbSupplierList
        {
            get { return cbSupplierList; }
            set { cbSupplierList = value; }
        }
        public DBOrderViewModel()
        {
            _shopService = new ShopService();
            _employeeService = new EmployeeService();
            _supplierService = new SupplierService();
            CbShopList = ObjMethods.CreateComboboxList<ShopDTO, ComboboxItem>(_shopService.GetAll(), "ShopId", "BuildingName");
            CbEmployeeList = ObjMethods.CreateComboboxList<EmployeeDTO, ComboboxItem>(_employeeService.GetAll(), "EmployeeId", "Firstname", "Lastname");
            CbSupplierList = ObjMethods.CreateComboboxList<SupplierDTO, ComboboxItem>(_supplierService.GetAll(), "SupplierId", "Firstname", "Lastname","Organisation");

        }

    }
}

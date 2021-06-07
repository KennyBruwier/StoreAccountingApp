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
    public class SaleService : BaseService<SaleDTO,Sale>
    {
        private ClientService _clientService;
        private ShopService _shopService;
        private EmployeeService _employeeService;
        public SaleService()
        {
            _clientService = new ClientService();
            _shopService = new ShopService();
            _employeeService = new EmployeeService();
        }
        public override SaleDTO CopyDBtoDTO(Sale source)
        {
            SaleDTO newSaleDTO = null;
            if (source != null)
            {
                newSaleDTO = ObjMethods.CopyProperties<Sale, SaleDTO>(source);
                if (newSaleDTO.ClientId != 0)
                    newSaleDTO.ClientFullname = _clientService.Search(newSaleDTO.ClientId, "Firstname", "Lastname");
                if (newSaleDTO.EmployeeId != 0)
                    newSaleDTO.EmployeeFullname = _employeeService.Search(newSaleDTO.EmployeeId,"Firstname","Lastname");
                if (newSaleDTO.ShopId != 0)
                    newSaleDTO.ShopName = _shopService.Search(newSaleDTO.ShopId, "BuildingName");
            }
            return newSaleDTO;
        }
    }
}

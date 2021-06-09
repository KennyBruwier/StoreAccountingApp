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
        public SaleService()
        {
        }
        public override SaleDTO CopyDBtoDTO(Sale source)
        {
            SaleDTO newSaleDTO = null;
            if (source != null)
            {
                newSaleDTO = ObjMethods.CopyProperties<Sale, SaleDTO>(source);
                if (newSaleDTO.ClientId != 0)
                {
                    ClientService clientSevice = new ClientService();
                    newSaleDTO.ClientDTO = clientSevice.Search(newSaleDTO.ClientId);
                    newSaleDTO.ClientFullname = String.Format("{0} {1}", newSaleDTO.ClientDTO.Firstname, newSaleDTO.ClientDTO.Lastname);
                }
                if (newSaleDTO.EmployeeId != 0)
                {
                    EmployeeService employeeService = new EmployeeService();
                    newSaleDTO.EmployeeDTO = employeeService.Search(newSaleDTO.EmployeeId);
                    newSaleDTO.EmployeeFullname = String.Format("{0} {1}", newSaleDTO.EmployeeDTO.Firstname, newSaleDTO.EmployeeDTO.Lastname);
                }
                if (newSaleDTO.ShopId != 0)
                {
                    ShopService shopService = new ShopService();
                    newSaleDTO.ShopDTO = shopService.Search(newSaleDTO.ShopId);
                    newSaleDTO.ShopName = newSaleDTO.ShopDTO.BuildingName;
                }
            }
            return newSaleDTO;
        }
    }
}

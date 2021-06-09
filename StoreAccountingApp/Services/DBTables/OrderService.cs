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
    public class OrderService : BaseService<OrderDTO,Order>
    {
        public OrderService()
        {

        }
        public override OrderDTO CopyDBtoDTO(Order source)
        {
            OrderDTO orderDTO = ObjMethods.CopyProperties<Order, OrderDTO>(source);
            if (orderDTO.EmployeeId != 0)
            {
                EmployeeService employeeService = new EmployeeService();
                orderDTO.EmployeeDTO = employeeService.Search(orderDTO.EmployeeId);
                orderDTO.EmployeeFullname = String.Format("{0} {1}", orderDTO.EmployeeDTO.Firstname, orderDTO.EmployeeDTO.Lastname);
            }
            if (orderDTO.ShopId != 0)
            {
                ShopService shopService = new ShopService();
                orderDTO.ShopDTO = shopService.Search(orderDTO.ShopId);
                orderDTO.ShopName = orderDTO.ShopDTO?.BuildingName;
            }
            if (orderDTO.SupplierId != 0)
            {
                SupplierService supplierService = new SupplierService();
                orderDTO.SupplierDTO = supplierService.Search(orderDTO.SupplierId);
                orderDTO.SupplierName = String.Format("{0} {1} ({2})", orderDTO.SupplierDTO?.Firstname, orderDTO.SupplierDTO?.Lastname, orderDTO.SupplierDTO?.Organisation);
            }
            return orderDTO;
        }
    }
}

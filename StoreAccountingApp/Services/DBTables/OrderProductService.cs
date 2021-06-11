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
    public class OrderProductService : BaseService<OrderProductDTO,OrderProduct>
    {
        public OrderProductService()
        {

        }
        public override OrderProductDTO CopyDBtoDTO(OrderProduct source)
        {
            OrderProductDTO orderProductDTO = ObjMethods.CopyProperties<OrderProduct, OrderProductDTO>(source);
            if (orderProductDTO.OrderId != 0)
            {
                OrderService orderService = new OrderService();
                orderProductDTO.OrderDTO = orderService.Search(orderProductDTO.OrderId);
                orderProductDTO.OrderInvoiceNr = orderProductDTO.OrderDTO?.InvoiceNumber;
                if (orderProductDTO.ProductId != 0)
                {
                    ProductService productService = new ProductService();
                    orderProductDTO.ProductDTO = productService.Search(orderProductDTO.ProductId);
                    orderProductDTO.ProductName = orderProductDTO.ProductDTO?.Name;
                }
            }
            return orderProductDTO;
        }
        public override OrderProduct CopyDTOtoDB(OrderProductDTO source)
        {
            OrderProduct orderProduct = ObjMethods.CopyProperties<OrderProductDTO, OrderProduct>(source);
            if (orderProduct.OrderId != 0)
            {
                Order order = ctx.Orders.Find(orderProduct.OrderId);
                if (order != null)
                {

                }
            }

            return orderProduct;
        }

        //public override Client CopyDTOtoDBtemp(ClientDTO dtoModel)
        //{
        //    Client newClient = ObjMethods.CopyProperties<ClientDTO, Client>(dtoModel);
        //    if (dtoModel.PostalCodeId != "")
        //    {
        //        District newDistrict = ctx.Districts.Find(dtoModel.PostalCodeId);
        //        if (newDistrict == null)
        //        {
        //            newDistrict = new District()
        //            {
        //                PostalCodeId = dtoModel.PostalCodeId,
        //                Name = dtoModel.DistrictName
        //            };
        //            Country currentDistrictCountry;
        //            currentDistrictCountry = ctx.Countries.FirstOrDefault(c => c.Name.Equals(dtoModel.CountryName, StringComparison.OrdinalIgnoreCase));
        //            if (currentDistrictCountry == null)
        //            {
        //                currentDistrictCountry = new Country() { Name = dtoModel.CountryName };
        //            }
        //            newDistrict.Country = currentDistrictCountry;
        //        }
        //        newClient.District = newDistrict;
        //    }
        //    return newClient;
        //}

    }
}

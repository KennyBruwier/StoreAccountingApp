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
        //public override OrderProduct CopyDTOtoDB(OrderProductDTO source)
        //{
        //    OrderProduct orderProduct = ObjMethods.CopyProperties<OrderProductDTO,OrderProduct>(source);
        //    if (orderProduct.OrderId != 0)
        //    {
        //        orderProduct.
        //    }

        //    return base.CopyDTOtoDB(source);
        //}

    }
}

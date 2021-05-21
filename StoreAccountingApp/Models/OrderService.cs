using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.DBModels;
using StoreAccountingApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Models
{
    public class OrderService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public OrderService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<OrderDTO> GetAll()
        {
            List<OrderDTO> jobFunctionList = new List<OrderDTO>();
            var ObjQuery = from Order in ctx.Orders
                           select Order;
            foreach (var jobFunction in ObjQuery)
            {
                jobFunctionList.Add(ObjMethods.CopyProperties<Order, OrderDTO>(jobFunction));
            }
            return jobFunctionList;
        }
        public bool Add(OrderDTO newOrderDTO)
        {
            //                                                          <----- Add validations here
            if ((newOrderDTO.OrderId != 0) || (newOrderDTO.InvoiceNumber != ""))
            {
                if (ctx.Orders.Find(newOrderDTO.OrderId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An order with id {newOrderDTO.OrderId} was already found, do you want to update it instead?",
                                                                    "Order already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newOrderDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newOrderDTO.OrderId} already exists");
                }
            }

            if (ctx.Orders.FirstOrDefault(a => (a.InvoiceNumber == newOrderDTO.InvoiceNumber)) != null)
                throw new ArgumentException($"Add operation failed, {newOrderDTO.InvoiceNumber} already exists");
            try
            {
                ctx.Orders.Add(ObjMethods.CopyProperties<OrderDTO, Order>(newOrderDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public OrderDTO Search(int jobFunctionId)
        {
            OrderDTO ObjOrder = null;
            var ObjOrderToFind = ctx.Orders.Find(jobFunctionId);
            if (ObjOrderToFind != null)
            {
                ObjOrder = ObjMethods.CopyProperties<Order, OrderDTO>(ObjOrderToFind);
            }
            return ObjOrder;
        }
        public bool Update(OrderDTO objOrderToUpdate)
        {
            var ObjOrder = ctx.Orders.Find(objOrderToUpdate.OrderId);
            if (ObjOrder != null)
            {
                ObjOrder = ObjMethods.CopyProperties<OrderDTO, Order>(objOrderToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int jobFunctionId)
        {
            var ObjOrderToDelete = ctx.Orders.Find(jobFunctionId);
            if (ObjOrderToDelete != null)
                ctx.Orders.Remove(ObjOrderToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

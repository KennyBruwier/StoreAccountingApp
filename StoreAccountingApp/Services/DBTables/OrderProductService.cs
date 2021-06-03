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

namespace StoreAccountingApp.Services
{
    public class OrderProductService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public OrderProductService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<OrderProductDTO> GetAll()
        {
            List<OrderProductDTO> orderProductList = new List<OrderProductDTO>();
            var ObjQuery = from OrderProduct in ctx.OrderProducts
                           select OrderProduct;
            foreach (var orderProduct in ObjQuery)
            {
                OrderProductDTO newOrderProductDTO = ObjMethods.CopyProperties<OrderProduct, OrderProductDTO>(orderProduct);

                orderProductList.Add(newOrderProductDTO);
            }
            return orderProductList;
        }
        public bool Add(OrderProductDTO newOrderProductDTO)
        {
            //                                                          <----- Add validations here
            if ((newOrderProductDTO.OrderId != 0) && (newOrderProductDTO.ProductId !=0))
            {
                if (ctx.OrderProducts.Find(newOrderProductDTO.OrderId, newOrderProductDTO.ProductId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An order product record with order id {newOrderProductDTO.OrderId} and product id {newOrderProductDTO.ProductId} was already found, do you want to update it instead?",
                                                                    "OrderProduct already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newOrderProductDTO);
                    else
                        throw new ArgumentException($"Add operation failed, order id {newOrderProductDTO.OrderId} and product id {newOrderProductDTO.ProductId} already exists");
                }
            }
            
            try
            {
                ctx.OrderProducts.Add(ObjMethods.CopyProperties<OrderProductDTO, OrderProduct>(newOrderProductDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public OrderProductDTO Search(int orderId, int productId)
        {
            OrderProductDTO ObjOrderProduct = null;
            var ObjOrderProductToFind = ctx.OrderProducts.Find(orderId,productId);
            if (ObjOrderProductToFind != null)
            {
                ObjOrderProduct = ObjMethods.CopyProperties<OrderProduct, OrderProductDTO>(ObjOrderProductToFind);
            }
            return ObjOrderProduct;
        }
        public bool Update(OrderProductDTO objOrderProductToUpdate)
        {
            var ObjOrderProduct = ctx.OrderProducts.Find(objOrderProductToUpdate.OrderId, objOrderProductToUpdate.ProductId);
            if (ObjOrderProduct != null)
            {
                ObjOrderProduct = ObjMethods.CopyProperties<OrderProductDTO, OrderProduct>(objOrderProductToUpdate);
            }
            try
            {
                return ctx.SaveChanges() > 0;
            }
            catch(DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int orderId, int productId)
        {
            var ObjOrderProductToDelete = ctx.OrderProducts.Find(orderId,productId);
            if (ObjOrderProductToDelete != null)
                ctx.OrderProducts.Remove(ObjOrderProductToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

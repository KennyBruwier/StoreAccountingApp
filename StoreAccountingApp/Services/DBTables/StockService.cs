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
    public class StockService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public StockService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<StockDTO> GetAll()
        {
            List<StockDTO> StockList = new List<StockDTO>();
            var ObjQuery = from Stock in ctx.Stocks
                           select Stock;
            foreach (var Stock in ObjQuery)
            {
                StockList.Add(ObjMethods.CopyProperties<Stock, StockDTO>(Stock));
            }
            return StockList;
        }
        public bool Add(StockDTO newStockDTO)
        {
            //                                                          <----- Add validations here
            if ((newStockDTO.StoreId != 0) && (newStockDTO.ProductId != 0))
            {
                if (ctx.Stocks.Find(newStockDTO.StoreId, newStockDTO.ProductId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An stock product record with stock id {newStockDTO.StoreId} and product id {newStockDTO.ProductId} was already found, do you want to update it instead?",
                                                                    "Stock already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newStockDTO);
                    else
                        throw new ArgumentException($"Add operation failed, stock id {newStockDTO.StoreId} and product id {newStockDTO.ProductId} already exists");
                }
            }

            try
            {
                ctx.Stocks.Add(ObjMethods.CopyProperties<StockDTO, Stock>(newStockDTO));
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
        public StockDTO Search(int stockId, int productId)
        {
            StockDTO ObjStock = null;
            var ObjStockToFind = ctx.Stocks.Find(stockId, productId);
            if (ObjStockToFind != null)
            {
                ObjStock = ObjMethods.CopyProperties<Stock, StockDTO>(ObjStockToFind);
            }
            return ObjStock;
        }
        public bool Update(StockDTO objStockToUpdate)
        {
            var ObjStock = ctx.Stocks.Find(objStockToUpdate.StoreId, objStockToUpdate.ProductId);
            if (ObjStock != null)
            {
                ObjStock = ObjMethods.CopyProperties<StockDTO, Stock>(objStockToUpdate);
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
        public bool Delete(int stockId, int productId)
        {
            var ObjStockToDelete = ctx.Stocks.Find(stockId, productId);
            if (ObjStockToDelete != null)
                ctx.Stocks.Remove(ObjStockToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

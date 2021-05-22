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
    public class SaleProductService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public SaleProductService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<SaleProductDTO> GetAll()
        {
            List<SaleProductDTO> saleProductList = new List<SaleProductDTO>();
            var ObjQuery = from SaleProduct in ctx.SaleProducts
                           select SaleProduct;
            foreach (var saleProduct in ObjQuery)
            {
                saleProductList.Add(ObjMethods.CopyProperties<SaleProduct, SaleProductDTO>(saleProduct));
            }
            return saleProductList;
        }
        public bool Add(SaleProductDTO newSaleProductDTO)
        {
            //                                                          <----- Add validations here
            if ((newSaleProductDTO.SaleId != 0) && (newSaleProductDTO.ProductId != 0))
            {
                if (ctx.SaleProducts.Find(newSaleProductDTO.SaleId, newSaleProductDTO.ProductId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An sale product record with sale id {newSaleProductDTO.SaleId} and product id {newSaleProductDTO.ProductId} was already found, do you want to update it instead?",
                                                                    "SaleProduct already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newSaleProductDTO);
                    else
                        throw new ArgumentException($"Add operation failed, sale id {newSaleProductDTO.SaleId} and product id {newSaleProductDTO.ProductId} already exists");
                }
            }

            try
            {
                ctx.SaleProducts.Add(ObjMethods.CopyProperties<SaleProductDTO, SaleProduct>(newSaleProductDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SaleProductDTO Search(int saleId, int productId)
        {
            SaleProductDTO ObjSaleProduct = null;
            var ObjSaleProductToFind = ctx.SaleProducts.Find(saleId, productId);
            if (ObjSaleProductToFind != null)
            {
                ObjSaleProduct = ObjMethods.CopyProperties<SaleProduct, SaleProductDTO>(ObjSaleProductToFind);
            }
            return ObjSaleProduct;
        }
        public bool Update(SaleProductDTO objSaleProductToUpdate)
        {
            var ObjSaleProduct = ctx.SaleProducts.Find(objSaleProductToUpdate.SaleId, objSaleProductToUpdate.ProductId);
            if (ObjSaleProduct != null)
            {
                ObjSaleProduct = ObjMethods.CopyProperties<SaleProductDTO, SaleProduct>(objSaleProductToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int saleId, int productId)
        {
            var ObjSaleProductToDelete = ctx.SaleProducts.Find(saleId, productId);
            if (ObjSaleProductToDelete != null)
                ctx.SaleProducts.Remove(ObjSaleProductToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

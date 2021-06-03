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
    public class SupplierProductService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public SupplierProductService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<SupplierProductDTO> GetAll()
        {
            List<SupplierProductDTO> SupplierProductList = new List<SupplierProductDTO>();
            var ObjQuery = from SupplierProduct in ctx.SupplierProducts
                           select SupplierProduct;
            foreach (var SupplierProduct in ObjQuery)
            {
                SupplierProductList.Add(ObjMethods.CopyProperties<SupplierProduct, SupplierProductDTO>(SupplierProduct));
            }
            return SupplierProductList;
        }
        public bool Add(SupplierProductDTO newSupplierProductDTO)
        {
            //                                                          <----- Add validations here
            if ((newSupplierProductDTO.SupplierId != 0) && (newSupplierProductDTO.ProductId != 0))
            {
                if (ctx.SupplierProducts.Find(newSupplierProductDTO.SupplierId, newSupplierProductDTO.ProductId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An supplier product record with supplier id {newSupplierProductDTO.SupplierId} and product id {newSupplierProductDTO.ProductId} was already found, do you want to update it instead?",
                                                                    "SupplierProduct already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newSupplierProductDTO);
                    else
                        throw new ArgumentException($"Add operation failed, supplier id {newSupplierProductDTO.SupplierId} and product id {newSupplierProductDTO.ProductId} already exists");
                }
            }

            try
            {
                ctx.SupplierProducts.Add(ObjMethods.CopyProperties<SupplierProductDTO, SupplierProduct>(newSupplierProductDTO));
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
        public SupplierProductDTO Search(int supplierId, int productId)
        {
            SupplierProductDTO ObjSupplierProduct = null;
            var ObjSupplierProductToFind = ctx.SupplierProducts.Find(supplierId, productId);
            if (ObjSupplierProductToFind != null)
            {
                ObjSupplierProduct = ObjMethods.CopyProperties<SupplierProduct, SupplierProductDTO>(ObjSupplierProductToFind);
            }
            return ObjSupplierProduct;
        }
        public bool Update(SupplierProductDTO objSupplierProductToUpdate)
        {
            var ObjSupplierProduct = ctx.SupplierProducts.Find(objSupplierProductToUpdate.SupplierId, objSupplierProductToUpdate.ProductId);
            if (ObjSupplierProduct != null)
            {
                ObjSupplierProduct = ObjMethods.CopyProperties<SupplierProductDTO, SupplierProduct>(objSupplierProductToUpdate);
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
        public bool Delete(int supplierId, int productId)
        {
            var ObjSupplierProductToDelete = ctx.SupplierProducts.Find(supplierId, productId);
            if (ObjSupplierProductToDelete != null)
                ctx.SupplierProducts.Remove(ObjSupplierProductToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

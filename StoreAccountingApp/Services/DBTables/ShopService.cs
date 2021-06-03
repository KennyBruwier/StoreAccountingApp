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
    public class ShopService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public ShopService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<ShopDTO> GetAll()
        {
            List<ShopDTO> ShopList = new List<ShopDTO>();
            var ObjQuery = from Shop in ctx.Shops
                           select Shop;
            foreach (var shop in ObjQuery)
            {
                ShopList.Add(ObjMethods.CopyProperties<Shop, ShopDTO>(shop));
            }
            return ShopList;
        }
        public bool Add(ShopDTO newShopDTO)
        {
            //                                                          <----- Add validations here
            if (newShopDTO.ShopId != 0)
            {
                if (ctx.Shops.Find(newShopDTO.ShopId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An store record with store id {newShopDTO.ShopId} was already found, do you want to update it instead?",
                                                                    "Shop already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newShopDTO);
                    else
                        throw new ArgumentException($"Add operation failed, store id {newShopDTO.ShopId} already exists");
                }
            }

            try
            {
                ctx.Shops.Add(ObjMethods.CopyProperties<ShopDTO, Shop>(newShopDTO));
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
        public ShopDTO Search(int storeId)
        {
            ShopDTO ObjShop = null;
            var ObjShopToFind = ctx.Shops.Find(storeId);
            if (ObjShopToFind != null)
            {
                ObjShop = ObjMethods.CopyProperties<Shop, ShopDTO>(ObjShopToFind);
            }
            return ObjShop;
        }
        public bool Update(ShopDTO objShopToUpdate)
        {
            var ObjShop = ctx.Shops.Find(objShopToUpdate.ShopId);
            if (ObjShop != null)
            {
                ObjShop = ObjMethods.CopyProperties<ShopDTO, Shop>(objShopToUpdate);
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
        public bool Delete(int storeId)
        {
            var ObjShopToDelete = ctx.Shops.Find(storeId);
            if (ObjShopToDelete != null)
                ctx.Shops.Remove(ObjShopToDelete);
            return ctx.SaveChanges() > 0;
        }

    }
}

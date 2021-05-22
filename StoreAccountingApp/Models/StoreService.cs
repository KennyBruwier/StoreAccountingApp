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
    public class StoreService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public StoreService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<StoreDTO> GetAll()
        {
            List<StoreDTO> StoreList = new List<StoreDTO>();
            var ObjQuery = from Store in ctx.Stores
                           select Store;
            foreach (var Store in ObjQuery)
            {
                StoreList.Add(ObjMethods.CopyProperties<Store, StoreDTO>(Store));
            }
            return StoreList;
        }
        public bool Add(StoreDTO newStoreDTO)
        {
            //                                                          <----- Add validations here
            if (newStoreDTO.StoreId != 0)
            {
                if (ctx.Stores.Find(newStoreDTO.StoreId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An store record with store id {newStoreDTO.StoreId} was already found, do you want to update it instead?",
                                                                    "Store already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newStoreDTO);
                    else
                        throw new ArgumentException($"Add operation failed, store id {newStoreDTO.StoreId} already exists");
                }
            }

            try
            {
                ctx.Stores.Add(ObjMethods.CopyProperties<StoreDTO, Store>(newStoreDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StoreDTO Search(int storeId)
        {
            StoreDTO ObjStore = null;
            var ObjStoreToFind = ctx.Stores.Find(storeId);
            if (ObjStoreToFind != null)
            {
                ObjStore = ObjMethods.CopyProperties<Store, StoreDTO>(ObjStoreToFind);
            }
            return ObjStore;
        }
        public bool Update(StoreDTO objStoreToUpdate)
        {
            var ObjStore = ctx.Stores.Find(objStoreToUpdate.StoreId);
            if (ObjStore != null)
            {
                ObjStore = ObjMethods.CopyProperties<StoreDTO, Store>(objStoreToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int storeId)
        {
            var ObjStoreToDelete = ctx.Stores.Find(storeId);
            if (ObjStoreToDelete != null)
                ctx.Stores.Remove(ObjStoreToDelete);
            return ctx.SaveChanges() > 0;
        }

    }
}

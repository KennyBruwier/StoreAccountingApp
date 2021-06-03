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
    public class SupplierService
    {
        private _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public SupplierService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<SupplierDTO> GetAll()
        {
            List<SupplierDTO> SupplierList = new List<SupplierDTO>();
            var ObjQuery = from Supplier in ctx.Suppliers
                           select Supplier;
            foreach (var Supplier in ObjQuery)
            {
                SupplierList.Add(ObjMethods.CopyProperties<Supplier, SupplierDTO>(Supplier));
            }
            return SupplierList;
        }
        public bool Add(SupplierDTO newSupplierDTO)
        {
            //                                                          <----- Add validations here
            if (newSupplierDTO.SupplierId != 0) 
            {
                if (ctx.Suppliers.Find(newSupplierDTO.SupplierId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An supplier product record with supplier id {newSupplierDTO.SupplierId} was already found, do you want to update it instead?",
                                                                    "Supplier already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newSupplierDTO);
                    else
                        throw new ArgumentException($"Add operation failed, supplier id {newSupplierDTO.SupplierId} already exists");
                }
            }

            try
            {
                ctx.Suppliers.Add(ObjMethods.CopyProperties<SupplierDTO, Supplier>(newSupplierDTO));
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
        public SupplierDTO Search(int supplierId)
        {
            SupplierDTO ObjSupplier = null;
            var ObjSupplierToFind = ctx.Suppliers.Find(supplierId);
            if (ObjSupplierToFind != null)
            {
                ObjSupplier = ObjMethods.CopyProperties<Supplier, SupplierDTO>(ObjSupplierToFind);
            }
            return ObjSupplier;
        }
        public bool Update(SupplierDTO objSupplierToUpdate)
        {
            var ObjSupplier = ctx.Suppliers.Find(objSupplierToUpdate.SupplierId);
            if (ObjSupplier != null)
            {
                ObjSupplier = ObjMethods.CopyProperties<SupplierDTO, Supplier>(objSupplierToUpdate);
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
        public bool Delete(int supplierId)
        {
            var ObjSupplierToDelete = ctx.Suppliers.Find(supplierId);
            if (ObjSupplierToDelete != null)
                ctx.Suppliers.Remove(ObjSupplierToDelete);
            return ctx.SaveChanges() > 0;
        }

    }
}

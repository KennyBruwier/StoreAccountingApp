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
    public class SaleService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public SaleService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<SaleDTO> GetAll()
        {
            List<SaleDTO> saleList = new List<SaleDTO>();
            var ObjQuery = from Sale in ctx.Sales
                           select Sale;
            foreach (var sale in ObjQuery)
            {
                saleList.Add(ObjMethods.CopyProperties<Sale, SaleDTO>(sale));
            }
            return saleList;
        }
        public bool Add(SaleDTO newSaleDTO)
        {
            //                                                          <----- Add validations here
            if (newSaleDTO.SaleId != 0)
            {
                if (ctx.Sales.Find(newSaleDTO.SaleId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"A sale with id {newSaleDTO.SaleId} was already found, do you want to update it instead?",
                                                                    "Sale already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newSaleDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newSaleDTO.SaleId} already exists");
                }
            }
            if (newSaleDTO.InvoiceNumber != "")
            {
                Sale ExistingInvoiceNumber = ctx.Sales.FirstOrDefault(a => a.InvoiceNumber == newSaleDTO.InvoiceNumber);
                if (ExistingInvoiceNumber != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"A sale with an invoice number {newSaleDTO.InvoiceNumber} was already found, do you want to update it instead?",
                                                                    "Sale with similar invoice number already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        newSaleDTO.SaleId = ExistingInvoiceNumber.SaleId;
                        return Update(newSaleDTO);
                    }
                    else
                        throw new ArgumentException($"Add operation failed, invoice number {newSaleDTO.InvoiceNumber} already exists");
                }
            }
            try
            {
                ctx.Sales.Add(ObjMethods.CopyProperties<SaleDTO, Sale>(newSaleDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SaleDTO Search(int saleId)
        {
            SaleDTO ObjSale = null;
            var ObjSaleToFind = ctx.Sales.Find(saleId);
            if (ObjSaleToFind != null)
            {
                ObjSale = ObjMethods.CopyProperties<Sale, SaleDTO>(ObjSaleToFind);
            }
            return ObjSale;
        }
        public bool Update(SaleDTO objSaleToUpdate)
        {
            var ObjSale = ctx.Sales.Find(objSaleToUpdate.SaleId);
            if (ObjSale != null)
            {
                ObjSale = ObjMethods.CopyProperties<SaleDTO, Sale>(objSaleToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int saleId)
        {
            var ObjSaleToDelete = ctx.Sales.Find(saleId);
            if (ObjSaleToDelete != null)
                ctx.Sales.Remove(ObjSaleToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

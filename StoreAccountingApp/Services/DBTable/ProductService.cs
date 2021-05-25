using StoreAccountingApp.CustomMethods;
using StoreAccountingApp.Models;
using StoreAccountingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.Services
{
    public class ProductService
    {
        _DBStoreAccountingContext ctx;

        public object DialogResult { get; private set; }
        public ProductService()
        {
            ctx = new _DBStoreAccountingContext();
        }
        public List<ProductDTO> GetAll()
        {
            List<ProductDTO> productList = new List<ProductDTO>();
            var ObjQuery = from Product in ctx.Products
                           select Product;
            foreach (var product in ObjQuery)
            {
                productList.Add(ObjMethods.CopyProperties<Product, ProductDTO>(product));
            }
            return productList;
        }
        public bool Add(ProductDTO newProductDTO)
        {
            //                                                          <----- Add validations here
            if (newProductDTO.ProductId != 0)
            {
                if (ctx.Products.Find(newProductDTO.ProductId) != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"A product with id {newProductDTO.ProductId} was already found, do you want to update it instead?",
                                                                    "Product already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        return Update(newProductDTO);
                    else
                        throw new ArgumentException($"Add operation failed, id {newProductDTO.ProductId} already exists");
                }
            }
            if (newProductDTO.Name != "")
            {
                Product ExistingInvoiceNumber = ctx.Products.FirstOrDefault(a => a.Name == newProductDTO.Name);
                if (ExistingInvoiceNumber != null)
                {
                    MessageBoxResult dialogResult = MessageBox.Show($"An product with a name {newProductDTO.Name} was already found, do you want to update it instead?",
                                                                    "Product with similar invoice number already exists", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        newProductDTO.ProductId = ExistingInvoiceNumber.ProductId;
                        return Update(newProductDTO);
                    }
                    else
                        throw new ArgumentException($"Add operation failed, invoice number {newProductDTO.Name} already exists");
                }
            }
            try
            {
                ctx.Products.Add(ObjMethods.CopyProperties<ProductDTO, Product>(newProductDTO));
                return ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProductDTO Search(int productId)
        {
            ProductDTO ObjProduct = null;
            var ObjProductToFind = ctx.Products.Find(productId);
            if (ObjProductToFind != null)
            {
                ObjProduct = ObjMethods.CopyProperties<Product, ProductDTO>(ObjProductToFind);
            }
            return ObjProduct;
        }
        public bool Update(ProductDTO objProductToUpdate)
        {
            var ObjProduct = ctx.Products.Find(objProductToUpdate.ProductId);
            if (ObjProduct != null)
            {
                ObjProduct = ObjMethods.CopyProperties<ProductDTO, Product>(objProductToUpdate);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool Delete(int productId)
        {
            var ObjProductToDelete = ctx.Products.Find(productId);
            if (ObjProductToDelete != null)
                ctx.Products.Remove(ObjProductToDelete);
            return ctx.SaveChanges() > 0;
        }
    }
}

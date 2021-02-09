using LiteDB;
using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Classes.Events;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace nu3ProductUpdate.Data.Services
{
    public class ProductsService : IProductsService
    {
        private ILiteCollection<Product> _productCollection;

        public ProductsService(IDbContext context)
        {
            _productCollection = GetCollection(context.Database);
        }

        public bool Exists(System.Linq.Expressions.Expression<System.Func<Product, bool>> predicate)
        {
            return _productCollection.Exists(predicate);
        }

        public IEnumerable<Product> FindAll()
        {
            return _productCollection.FindAll();
        }

        public Product GetByHandle(string handle)
        {
            return _productCollection.FindOne(item => item.Handle == handle);
        }

        public long Insert(Product product)
        {
            return _productCollection.Insert(product);
        }

        public void Subscribe(IFilesService fileService)
        {
            fileService.OnFileUploaded += FileService_OnFileUploaded;
        }

        public bool Update(Product product)
        {
            return _productCollection.Update(product);
        }

        private void FileService_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (e.MimeType == AllowedMimeTypes.Text.Xml)
            {
                var products = GetProductsFromXml(e.File);
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        var currentProduct = GetByHandle(product.Handle);
                        if (currentProduct != null)
                        {
                            if (!currentProduct.Equals(product))
                            {
                                Update(product);
                            }
                        }
                        else
                        {
                            Insert(product);
                        }
                    }
                }
            }
        }

        private static Product[] GetProductsFromXml(Stream stream)
        {
            Product[] retVal = null;
            stream.Position = 0;
            XmlSerializer serializer = new XmlSerializer(typeof(Products));
            var products = (Products)serializer.Deserialize(stream);

            if (products != null && products.Items != null && products.Items.Length > 0)
            {
                retVal = products.Items;
            }

            return retVal;
        }

        private ILiteCollection<Product> GetCollection(LiteDatabase productDb)
        {
            return productDb.GetCollection<Product>("Products");
        }
    }
}
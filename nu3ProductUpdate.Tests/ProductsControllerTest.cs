using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nu3ProductUpdate.Controllers;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace nu3ProductUpdate.Tests
{
    [TestClass]
    public class ProductsControllerTest
    {
        private Mock<IProductsService> productsService = new Mock<IProductsService>();
        private Mock<IInventoryService> inventoryService = new Mock<IInventoryService>();
        private Mock<IFilesService> filesService = new Mock<IFilesService>();
        private Mock<ILogger<FilesController>> logger = new Mock<ILogger<FilesController>>();

        public ProductsControllerTest()
        {
            productsService.Setup(p => p.FindAll()).Returns(ProductListForTests);
            productsService.Setup(p => p.GetByHandle("test1")).Returns(ProductListForTests[0]);
            productsService.Setup(p => p.Update(It.IsAny<Product>())).Returns(true);
            productsService.Setup(p => p.Insert(It.IsAny<Product>())).Returns<Product>(x => x.Id);
        }

        [TestMethod]
        public void GetSingleProduct()
        {
            ProductsController productsController = new ProductsController(productsService.Object);

            var product = productsController.Get("test1");
            Assert.IsTrue(((product?.Result as OkObjectResult)?.Value as Product).Equals(ProductListForTests[0]));

        }

        [TestMethod]
        public void GetAllProducts()
        {
            ProductsController productsController = new ProductsController(productsService.Object);

            var products = productsController.Get();
            var productCount = products.Count();

            Assert.AreEqual(productCount, 2);
            Assert.IsTrue(products.ElementAt(0).Equals(ProductListForTests[0]));
            Assert.IsTrue(products.ElementAt(1).Equals(ProductListForTests[1]));
        }

        [TestMethod]
        public void UpdateProduct()
        {
            ProductsController productsController = new ProductsController(productsService.Object);

            var firstProduct = ProductListForTests[1];
            var result = productsController.Update(firstProduct) as NoContentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void InsertProduct()
        {
            ProductsController productsController = new ProductsController(productsService.Object);

            var firstProduct = ProductListForTests[1];
            var result = productsController.Insert(firstProduct);

            var productInsertResult = (result as CreatedAtRouteResult)?.Value;
            dynamic dynamicResult = productInsertResult;
            Assert.IsNotNull(productInsertResult);
            Assert.AreEqual(dynamicResult.Id, firstProduct.Id);
        }

        private Product[] ProductListForTests
        {
            get
            {
                var productList = new Product[] {
                new Product {
                    Amount = 10,
                    Bodyhtml = "",
                    CreatedAt = DateTime.MinValue,
                    Handle = "test1", Id = 0,
                    Image = new Image{ Alt="alt", CreateDate= DateTime.MinValue, Height=1, Id=1, Position=0, ProductId=1, Src="", UpdatedAt= DateTime.MinValue, Width=1 },
                    Location = "BERLIN",
                    Producttype = "",
                    PublishedScope = "",
                    Tags = "", Title = "",
                    Vendor = "" }
                ,
                new Product {  Amount = 10,
                    Bodyhtml = "",
                    CreatedAt = DateTime.MinValue,
                    Handle = "test1", Id = 1,
                    Image = new Image{ Alt="alt", CreateDate= DateTime.MinValue, Height=1, Id=1, Position=0, ProductId=1, Src="", UpdatedAt= DateTime.MinValue, Width=1 },
                    Location = "BERLIN",
                    Producttype = "",
                    PublishedScope = "",
                    Tags = "", Title = "",
                    Vendor = ""} };

                return productList;
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nu3ProductUpdate.Controllers;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace nu3ProductUpdate.Tests
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void GetSingleProduct()
        {
            var productList = new Product[] {
                new Product {
                    Amount = 10,
                    Bodyhtml = "",
                    CreatedAt = DateTime.Now,
                    Handle = "test1", Id = 0,
                    Image = new Image{ Alt="alt", CreateDate= DateTime.Now, Height=1, Id=1, Position=0, ProductId=1, Src="", UpdatedAt= DateTime.Now, Width=1 },
                    Location = "BERLIN",
                    Producttype = "",
                    PublishedScope = "",
                    Tags = "", Title = "",
                    Vendor = "" }
                ,
                new Product {  Amount = 10,
                    Bodyhtml = "",
                    CreatedAt = DateTime.Now,
                    Handle = "test1", Id = 1,
                    Image = new Image{ Alt="alt", CreateDate= DateTime.Now, Height=1, Id=1, Position=0, ProductId=1, Src="", UpdatedAt= DateTime.Now, Width=1 },
                    Location = "BERLIN",
                    Producttype = "",
                    PublishedScope = "",
                    Tags = "", Title = "",
                    Vendor = ""} };
            Mock<IProductsService> productsService = new Mock<IProductsService>();
            productsService.Setup(p => p.FindAll()).Returns(productList);

            productsService.Setup(p => p.GetByHandle("test1")).Returns(productList[0]);

            Mock<IInventoryService> inventoryService = new Mock<IInventoryService>();
            Mock<IFilesService> filesService = new Mock<IFilesService>();
            Mock<ILogger<FilesController>> logger = new Mock<ILogger<FilesController>>();



            ProductsController productsController = new ProductsController(productsService.Object);
            //FilesController filesController = new FilesController(logger.Object, filesService.Object, productsService.Object, inventoryService.Object);

            var x = productsController.Get("test1");
            Assert.AreEqual((x.Result as OkObjectResult).Value as Product, productList[0]);
        }

    }




}

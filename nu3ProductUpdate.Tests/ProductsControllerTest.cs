using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nu3ProductUpdate.Controllers;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System.Linq;
using System.Net;

namespace nu3ProductUpdate.Tests
{
    [TestClass]
    public class ProductsControllerTest
    {
        private Mock<IProductsService> productsService = new Mock<IProductsService>();
        private static readonly Product[] ProductListForTests = MockData.ProductsList;

        public ProductsControllerTest()
        {
            productsService.SetupProductsService(ProductListForTests);
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
    }
}
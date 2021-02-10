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
        private Mock<IProductsService> _productsService = new Mock<IProductsService>();
        private ProductsController _productsController;
        private static readonly Product[] _productData = MockData.ProductsList;
        public ProductsControllerTest()
        {
            _productsService.SetupProductsService(_productData);
            _productsController = new ProductsController(_productsService.Object);

        }

        [TestMethod]
        public void GetSingleProduct()
        {

            var product = _productsController.Get("test1");
            Assert.IsTrue(((product?.Result as OkObjectResult)?.Value as Product).Equals(_productData[0]));
        }

        [TestMethod]
        public void GetAllProducts()
        {
            var products = _productsController.Get();
            var productCount = products.Count();

            Assert.AreEqual(productCount, 2);
     

            bool allItemsEqual = true;

            for (int i = 0; i < productCount; i++)
            {
                if (!products.ElementAt(i).Equals(_productData.ElementAt(i)))
                {
                    allItemsEqual = false;
                    break;
                }
            }

            Assert.IsTrue(allItemsEqual);
        }

        [TestMethod]
        public void UpdateProduct()
        {

            var firstProduct = _productData[1];
            var result = _productsController.Update(firstProduct) as NoContentResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, (int)HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void InsertProduct()
        {

            var firstProduct = _productData[1];
            var result = _productsController.Insert(firstProduct);

            var productInsertResult = (result as CreatedAtRouteResult)?.Value;
            dynamic dynamicResult = productInsertResult;
            Assert.IsNotNull(productInsertResult);
            Assert.AreEqual(dynamicResult.Id, firstProduct.Id);
        }

        [TestMethod]
        public void GetSingleProductBadRequest()
        {
            var product = _productsController.Get("");
            var badRequestObjectResult = product.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestObjectResult);
        }


        [TestMethod]
        public void InsertProductBadRequest()
        {
            
            var result = _productsController.Insert(null);

            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestObjectResult);

        }

        [TestMethod]
        public void UpdateProductBadRequest()
        {
            
            var result = _productsController.Update(null);
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestObjectResult);
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nu3ProductUpdate.Controllers;
using nu3ProductUpdate.Data.Classes;
using nu3ProductUpdate.Data.Interfaces;
using System.Linq;
using System.Text;

namespace nu3ProductUpdate.Tests
{
    [TestClass]
    public class FilesControllerTest
    {
        private Mock<IFilesService> _filesService = new Mock<IFilesService>();
        private Mock<IInventoryService> _inventoryService = new Mock<IInventoryService>();
        private Mock<IProductsService> _productsService = new Mock<IProductsService>();

        private Mock<ILogger<FilesController>> _logger = new Mock<ILogger<FilesController>>();
        private FilesController _filesController;

        public FilesControllerTest()
        {
            _filesService.SetupFilesService(MockData.FilesList);
            _productsService.SetupProductsService(MockData.ProductsList);

            _filesController = new FilesController(_logger.Object, _filesService.Object, _productsService.Object, _inventoryService.Object);
        }

        [TestMethod]
        public void GetFiles()
        {
            Assert.AreEqual(_filesController.Get().Count(), 3);
        }

        [TestMethod]
        public void GetById()
        {
            var downloadedItem = _filesController.Download("test2");
            var fileStreamResult = downloadedItem as FileStreamResult;
            Assert.AreEqual(fileStreamResult.FileDownloadName, "test2.xml");
        }

        [TestMethod]
        public void Upload()
        {
            IFormFile file = new FormFile(new System.IO.MemoryStream(Encoding.UTF8.GetBytes("")), 0, 0, "test", "test.xml");

            var uploadedItem = _filesController.Upload(file);
            var okObjectResult = uploadedItem as OkObjectResult;
            Assert.IsNotNull(okObjectResult.Value as FileUploadResult);
        }
        [TestMethod]
        public void GetByIdBadRequest()
        {
            var downloadedItem = _filesController.Download("");
            var fileStreamResult = downloadedItem as BadRequestObjectResult;
            Assert.IsNotNull(fileStreamResult);
        }

        [TestMethod]
        public void UploadBadRequest()
        {
            IFormFile file = null;

            var uploadedItem = _filesController.Upload(file);
            var fileStreamResult = uploadedItem as BadRequestObjectResult;
            Assert.IsNotNull(fileStreamResult);
        }
    }
}
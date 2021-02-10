using Moq;
using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Data.Classes;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System.Collections.Generic;
using System.Linq;

namespace nu3ProductUpdate.Tests
{
    public static class TestExtensions
    {
        public static void SetupProductsService(this Mock<IProductsService> mock, IEnumerable<Product> productList)
        {
            mock.Setup(p => p.FindAll()).Returns(productList);
            mock.Setup(p => p.GetByHandle(It.IsAny<string>())).Returns((string handle) => productList.FirstOrDefault(item => item.Handle == handle));
            mock.Setup(p => p.Update(It.IsAny<Product>())).Returns(true);
            mock.Setup(p => p.Insert(It.IsAny<Product>())).Returns<Product>(x => x.Id);
        }

        public static void SetupFilesService(this Mock<IFilesService> mock, IEnumerable<CustomFileInfo> files)
        {
            var stream = new System.IO.MemoryStream();
            mock.Setup(f => f.FindAll()).Returns(files);
            mock.Setup(f => f.GetFileInfoById(It.IsAny<string>())).Returns<string>(x => files.FirstOrDefault(item => item.Id == x));
            mock.Setup(f => f.GetFileStreamById(It.IsAny<string>())).Returns(new CustomFileStream(stream));
            mock.Setup(f => f.Add(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<System.IO.Stream>())).Returns(files.FirstOrDefault());
        }

        public static void SetupInventoryService(this Mock<IInventoryService> mock, IEnumerable<Inventory> inventory)
        {
            mock.Setup(i => i.FindAll()).Returns(inventory);
            mock.Setup(i => i.GetByHandle(It.IsAny<string>())).Returns((string handle) => MockData.InventoryList.FirstOrDefault(item => item.Handle == handle));
        }

    }
}
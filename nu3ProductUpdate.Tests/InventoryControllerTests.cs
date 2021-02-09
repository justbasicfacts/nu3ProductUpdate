using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nu3ProductUpdate.Controllers;
using nu3ProductUpdate.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nu3ProductUpdate.Tests
{
    [TestClass]

    public class InventoryControllerTests
    {
        private Mock<IInventoryService> _inventoryService = new Mock<IInventoryService>();
        private InventoryController _inventoryController;

        public InventoryControllerTests()
        {
            _inventoryService.Setup(i => i.FindAll()).Returns(MockData.InventoryList);
            _inventoryService.Setup(i => i.GetByHandle(It.IsAny<string>())).Returns((string handle) => MockData.InventoryList.FirstOrDefault(item => item.Handle == handle));
            _inventoryController = new InventoryController(_inventoryService.Object);
        }

        [TestMethod]
        public void GetInventory()
        {
            var inventoryList = _inventoryController.Get();
            Assert.AreEqual(inventoryList.Count(), 3);
        }
    }
}

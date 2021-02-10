using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nu3ProductUpdate.Controllers;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
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
        private static readonly IEnumerable<Inventory> _inventoryList = MockData.InventoryList;
        private InventoryController _inventoryController;

        public InventoryControllerTests()
        {
            _inventoryService.SetupInventoryService(_inventoryList);
            _inventoryController = new InventoryController(_inventoryService.Object);
        }


        [TestMethod]
        public void GetInventory()
        {
            var inventoryList = _inventoryController.Get();
            Assert.AreEqual(inventoryList.Count(), 3);

            bool allItemsEqual = true;

            for (int i = 0; i < inventoryList.Count(); i++)
            {
                if (!inventoryList.ElementAt(i).Equals(_inventoryList.ElementAt(i)))
                {
                    allItemsEqual = false;
                    break;
                }
            }

            Assert.IsTrue(allItemsEqual);
        }
    }
}

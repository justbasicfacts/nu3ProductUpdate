using Microsoft.AspNetCore.Mvc;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System.Collections.Generic;

namespace nu3ProductUpdate.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public IEnumerable<Inventory> Get()
        {
            return _inventoryService.FindAll();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared;
using ShopWorld.Shared.Entities;

namespace ShopWorld.API.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemLogic _itemLogic;
        public ItemController(IItemLogic itemLogic) { 
            _itemLogic = itemLogic;
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(List<Item>))]
        public IActionResult _GetAllItems() {
            return Ok(_itemLogic.GetAllItems());
        }

        [HttpPost]
        [Produces("application/json",Type=typeof(Item))]
        public IActionResult _AddItem(ItemInputModel ItemToAdd)
        {
            return Ok(_itemLogic.AddItem(ItemToAdd));
        }
        
        [HttpGet]
        [Produces("application/json",Type=typeof(Item))]
        public IActionResult _GetItem(int ItemId)
        {
            return Ok(_itemLogic.GetItem(ItemId));
        }
        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _UpdateItem(ItemInputModel ItemToUpdate) { 
            return Ok(_itemLogic.UpdateItem(ItemToUpdate));
        }

        [HttpPost]
        [Produces("application/json",Type=typeof(bool))]
        public IActionResult _DeleteItem(int ItemId)
        {
            return Ok(_itemLogic.DeleteItem(ItemId));
        }
    }
}

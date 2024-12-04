using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared;
using ShopWorld.Shared.Models;
using System.Data;

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
        [Produces("application/json", Type = typeof(IEnumerable<ItemModel>))]
        public IActionResult _GetAllItems() {
            return Ok(_itemLogic.GetAllItems());
        }

        [HttpGet]
        [Produces("text/plain",Type = typeof(string))]
        public async Task<IActionResult> _GetBase64ImageForImageName(string ImageName)
        {
            string base64 = await _itemLogic.GetBase64ImageForImageName(ImageName);
            return Ok(base64);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        [HttpPost]
        [Produces("application/json",Type = typeof(ItemModel))]
        public IActionResult _AddItem(ItemInputModel ItemToAdd)
        {
            if(ModelState.IsValid)
            {
                return Ok(_itemLogic.AddItem(ItemToAdd));
            }
            return BadRequest(ModelState.Values);
        }
        
        [HttpGet]
        [Produces("application/json",Type = typeof(ItemModel))]
        public IActionResult _GetItem(int ItemId)
        {
            return Ok(_itemLogic.GetItem(ItemId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _UpdateItem(ItemInputModel ItemToUpdate) {
            if (ModelState.IsValid)
            {
                return Ok(_itemLogic.UpdateItem(ItemToUpdate));
            }
            return BadRequest(ModelState.Values);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        [Produces("application/json",Type = typeof(bool))]
        public IActionResult _DeleteItem(int ItemId)
        {
            return Ok(_itemLogic.DeleteItem(ItemId));
        }
    }
}

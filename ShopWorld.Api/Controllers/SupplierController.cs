using ShopWorld.BusinessLogic;
using ShopWorld.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ShopWorld.Shared.Models;

namespace ShopWorld.Api.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierLogic _supplierLogic;
        public SupplierController(ISupplierLogic supplierLogic)
        {
            _supplierLogic = supplierLogic;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<SupplierModel>))]
        public IActionResult _GetAllSuppliers()
        {
            return Ok(_supplierLogic.GetSuppliers());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<SupplierModel>))]
        public IActionResult _SearchSuppliers(string Search)
        {
            return Ok(_supplierLogic.SearchSuppliers(Search));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(SupplierModel))]
        public IActionResult _GetSupplierById(int SupplierId)
        {
            return Ok(_supplierLogic.GetSupplierById(SupplierId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json", Type = typeof(SupplierModel))]
        public IActionResult _AddSupplier(SupplierModel Supplier)
        {
            if (ModelState.IsValid)
            {
                return Ok(_supplierLogic.AddSupplier(Supplier));
            }
            return BadRequest(ModelState.PrintError());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _UpdateSupplier(SupplierModel Supplier)
        {
            if (ModelState.IsValid) {
                return Ok(_supplierLogic.UpdateSupplier(Supplier));
            }
            return BadRequest(ModelState.PrintError());
        }
    }
}

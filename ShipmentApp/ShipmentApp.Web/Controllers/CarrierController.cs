using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Contracts.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace ShipmentApp.Web.Controllers
{
    [Route("carrier")]
    public class CarrierController : Controller
    {
        private readonly ICarrierService carrierService;

        public CarrierController(ICarrierService carrierService)
        {
            this.carrierService = carrierService;
        }

        [HttpGet("index")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View();
        }

        [SwaggerOperation(Summary = "Create/Update carrier, returns status code")]
        [HttpPost("create")]
        public IActionResult Create(CarrierViewModel viewModel)
        {
            if (viewModel.Id == 0)
            {
                carrierService.Create(viewModel);
            }
            else
            {
                carrierService.Update(viewModel);
            }

            return Ok();
        }

        [SwaggerOperation(Summary = "Delete carrier, returns status code")]
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            carrierService.Delete(id);
            return Ok();
        }

        [SwaggerOperation(Summary = "Returns all carriers in JSON format")]
        [HttpGet("getall")]
        public JsonResult GetAll()
        {
            var carriers = carrierService.GetAll();
            return Json(carriers);
        }
    }
}
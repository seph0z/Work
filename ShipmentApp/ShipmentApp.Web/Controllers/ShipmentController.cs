using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Contracts.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace ShipmentApp.Web.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly IShipmentService shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            this.shipmentService = shipmentService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return View();
            }
                
            var shipments = shipmentService.Retrieve(id.Value);
            return View(shipments);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public IActionResult Create(ShipmentViewModel viewModel)
        {
            if (viewModel.Id == new Guid())
            {
                shipmentService.Create(viewModel);
            }
            else
            {
                shipmentService.Update(viewModel);
            }

            return RedirectToAction("Index");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Retrieve(Guid id)
        {
            var shipment = shipmentService.Retrieve(id);
            return View(shipment);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Delete(Guid id)
        {
            shipmentService.Delete(id);
            return RedirectToAction("Index");
        }

        [SwaggerOperation(Summary = "Returns all shipments in JSON format")]
        [EnableQuery()]
        public ActionResult<IQueryable<ShipmentViewModel>> GetAll()
        {
            var shipments = new ActionResult<IQueryable<ShipmentViewModel>>(shipmentService.RetrieveAll());
            return shipments;
        }
    }
}
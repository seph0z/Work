using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Contracts.ViewModels;

namespace ShipmentApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IActivityLogService activityLogService;
        public LogController(IActivityLogService activityLogService)
        {
            this.activityLogService = activityLogService;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ActivityLogViewModel>> Get(Guid id)
        {
            var logs = activityLogService.GetAll(id).ToList();
            return logs;
        }
    }
}
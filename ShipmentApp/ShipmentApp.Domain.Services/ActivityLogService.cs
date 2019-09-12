using Mapster;
using ShipmentApp.Data.Contracts.Entities;
using ShipmentApp.Data.EntityFramework;
using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Contracts.ViewModels;
using ShipmentApp.Domain.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ShipmentApp.Domain.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly AppDbContext context;

        public ActivityLogService(AppDbContext context)
        {
            this.context = context;
        }

        public void Create(ActivityLogViewModel entity)
        {
            if (entity == null)
            {
                throw new StatusCodeException(HttpStatusCode.BadRequest);
            }

            var result = entity.Adapt<ActivityLog>();
            context.ActivityLogs.Add(result);
            context.SaveChanges();
        }

        public IEnumerable<ActivityLogViewModel> GetAll(Guid id)
        {
            var activityLogs = (from logs in context.ActivityLogs.ToList()
                               where logs.ShipmentId == id
                               select logs).ToList();
            var result = activityLogs.Adapt<IEnumerable<ActivityLogViewModel>>();
            return result;
        }
    }
}

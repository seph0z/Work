using Mapster;
using ShipmentApp.Data.Contracts.Entities;
using ShipmentApp.Data.EntityFramework;
using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Contracts.ViewModels;
using ShipmentApp.Domain.Services.Exceptions;
using ShipmentApp.Domain.Services.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShipmentApp.Domain.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly AppDbContext dbContext;
        private readonly Tracker<ShipmentViewModel> tracker;

        public ShipmentService(AppDbContext dbContext, Tracker<ShipmentViewModel> tracker)
        {
            this.dbContext = dbContext;
            this.tracker = tracker;
        }

        public void Create(ShipmentViewModel shipment)
        {
            shipment.EnsureExists();
            var result = TypeAdapter.Adapt<ShipmentViewModel, Shipment>(shipment);
            dbContext.Shipments.Add(result);
            dbContext.SaveChanges();

            var logSHipment = result.Adapt<ShipmentCreate>();
            tracker.Track(logSHipment);
        }

        public void Delete(Guid id)
        {
            var result = dbContext.Shipments.Find(id);
            result.EnsureExists();

            if (result.SenderAddress != null)
            {
                dbContext.Addresses.Remove(result.SenderAddress);
            }

            if (result.RecipientAddress != null)
            {
                dbContext.Addresses.Remove(result.RecipientAddress);
            }

            dbContext.Shipments.Remove(result);
            dbContext.SaveChanges();

            var logSHipment = result.Adapt<ShipmentDelete>();
            tracker.Track(logSHipment);
        }

        public ShipmentViewModel Retrieve(Guid id)
        {
            var shipment = dbContext.Shipments.Find(id);
            shipment.EnsureExists();
            var result = TypeAdapter.Adapt<Shipment, ShipmentViewModel>(shipment);
            return result;
        }

        public IQueryable<ShipmentViewModel> RetrieveAll()
        {          
            var result = dbContext.Shipments.ProjectToType<ShipmentViewModel>();
            return result;
        }

        public void Update(ShipmentViewModel entity)
        {
            var shipment = dbContext.Shipments.Find(entity.Id);
            shipment.EnsureExists();
            var result = entity.Adapt(shipment);
            dbContext.Shipments.Update(result);
            dbContext.SaveChanges();

            var logSHipment = result.Adapt<ShipmentUpdate>();
            tracker.Track(logSHipment);
        }
    }
}

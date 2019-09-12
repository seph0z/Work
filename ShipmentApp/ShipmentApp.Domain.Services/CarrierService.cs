using Mapster;
using ShipmentApp.Data.Contracts.Entities;
using ShipmentApp.Data.EntityFramework;
using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Contracts.ViewModels;
using ShipmentApp.Domain.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ShipmentApp.Domain.Services
{
    public class CarrierService : ICarrierService
    {
        private readonly AppDbContext context;
        public CarrierService(AppDbContext context)
        {
            this.context = context;
        }

        public void Create(CarrierViewModel entity)
        {
            if(entity == null)
            {
                throw new StatusCodeException(HttpStatusCode.BadRequest);
            }

            var result = entity.Adapt<Carrier>();
            context.Carriers.Add(result);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var carrier = context.Carriers.Find(id);
            carrier.EnsureExists();
            context.Carriers.Remove(carrier);
            context.SaveChanges();
        }

        public CarrierViewModel Get(int id)
        {
            var carrier = context.Carriers.Find(id);
            carrier.EnsureExists();
            var result = carrier.Adapt<CarrierViewModel>();
            return result;
        }

        public IEnumerable<CarrierViewModel> GetAll()
        {
            var carriers = context.Carriers;
            var result = carriers.Adapt<IEnumerable<CarrierViewModel>>();
            return result;
        }

        public void Update(CarrierViewModel entity)
        {
            var carrier = context.Carriers.Find(entity.Id);
            carrier.EnsureExists();
            var result = entity.Adapt(carrier);
            context.Carriers.Update(result);
            context.SaveChanges();
        }
    }
}

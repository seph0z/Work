using ShipmentApp.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Contracts
{
    public interface ICarrierService
    {
        CarrierViewModel Get(int id);
        IEnumerable<CarrierViewModel> GetAll();
        void Create(CarrierViewModel entity);
        void Update(CarrierViewModel entity);
        void Delete(int id);
    }
}

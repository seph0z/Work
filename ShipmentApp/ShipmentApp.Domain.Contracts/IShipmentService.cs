using ShipmentApp.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShipmentApp.Domain.Contracts
{
    public interface IShipmentService
    {
        ShipmentViewModel Retrieve(Guid id);
        IQueryable<ShipmentViewModel> RetrieveAll();
        void Create(ShipmentViewModel entity);
        void Update(ShipmentViewModel entity);
        void Delete(Guid id);
    }
}

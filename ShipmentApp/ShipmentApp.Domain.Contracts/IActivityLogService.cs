using ShipmentApp.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Contracts
{
    public interface IActivityLogService
    {
        IEnumerable<ActivityLogViewModel> GetAll(Guid id);
        void Create(ActivityLogViewModel entity);
    }
}

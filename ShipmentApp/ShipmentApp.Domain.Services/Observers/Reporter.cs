using ShipmentApp.Domain.Contracts;
using ShipmentApp.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Services.Observers
{
    public class Reporter : IObserver<ShipmentViewModel>
    {
        private string _action;
        private IDisposable _unsubscriber;
        private readonly IActivityLogService _activityLogService;

        public Reporter(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        public virtual void Subscribe(IObservable<ShipmentViewModel> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(ShipmentViewModel value)
        {
            if (value is ShipmentCreate)
                _action = "Create";
            if (value is ShipmentUpdate)
                _action = "Update";
            if (value is ShipmentDelete)
                _action = "Delete";

            var log = new ActivityLogViewModel
            {               
                Action = _action,
                DateTime = DateTime.Now,
                ShipmentId = value.Id,
                CarrierId = value.CarrierId
            };

            _activityLogService.Create(log);
        }
    }   
}

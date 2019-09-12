using System;

namespace ShipmentApp.Domain.Contracts.ViewModels
{
    public class ActivityLogViewModel
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }

        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public Guid ShipmentId { get; set; }
    }
}

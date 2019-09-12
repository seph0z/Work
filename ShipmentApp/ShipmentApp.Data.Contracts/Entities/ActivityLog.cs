using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Data.Contracts.Entities
{
    public class ActivityLog
    {
        public Guid Id { get; set; }      
        public string Action { get; set; }
        public DateTime DateTime { get; set; }

        public Guid ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }

        public int CarrierId { get; set; }
        public virtual Carrier Carrier { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Data.Contracts.Entities
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }

    }
}

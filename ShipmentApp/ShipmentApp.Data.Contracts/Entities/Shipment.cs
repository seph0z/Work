using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShipmentApp.Data.Contracts.Entities
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public double Weight { get; set; }

        public Guid SenderAddressId { get; set; }
        public virtual Address SenderAddress { get; set; }

        public Guid RecipientAddressId { get; set; }
        public virtual Address RecipientAddress { get; set; }

        public int CarrierId { get; set; }
        public virtual Carrier Carrier { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
    }
}

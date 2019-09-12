using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Contracts.ViewModels
{
    public class ShipmentViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string SenderName { get; set; }
        public AddressViewModel SenderAddress { get; set; }
        public string RecipientName { get; set; }
        public AddressViewModel RecipientAddress { get; set; }
        public double Weight { get; set; }
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
    }

    public class ShipmentCreate : ShipmentViewModel
    {

    }

    public class ShipmentUpdate : ShipmentViewModel
    {

    }

    public class ShipmentDelete : ShipmentViewModel
    {

    }
}

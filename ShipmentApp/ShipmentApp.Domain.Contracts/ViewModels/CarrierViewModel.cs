using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Contracts.ViewModels
{
    public class CarrierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfShipments { get; set; }
    }
}

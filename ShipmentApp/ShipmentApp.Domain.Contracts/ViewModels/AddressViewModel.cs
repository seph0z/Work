using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Contracts.ViewModels
{
    public class AddressViewModel
    {
        public string PostCode { get; set; }
        public string CountryCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }
}

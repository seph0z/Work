using Mapster;
using ShipmentApp.Data.Contracts.Entities;
using ShipmentApp.Domain.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Services.MappingProfiles
{
    public static class MapsterConfig
    {
        public static void ConfigCarrier()
        {
            TypeAdapterConfig<Carrier, CarrierViewModel>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.NumberOfShipments, src => src.Shipments.Count);

            TypeAdapterConfig<ActivityLog, ActivityLogViewModel>.NewConfig()
                 .Map(dest => dest.Id, src => src.Id)
                 .Map(dest => dest.Action, src => src.Action)
                 .Map(dest => dest.DateTime, src => src.DateTime)
                 .Map(dest => dest.CarrierId, src => src.CarrierId)
                 .Map(dest => dest.ShipmentId, src => src.ShipmentId)
                 .Map(dest => dest.CarrierName, src => src.Carrier.Name);
        }
    }
}

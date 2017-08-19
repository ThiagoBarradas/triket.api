using System;
using System.Collections.Generic;
using System.Text;
using Trinket.Api.Models;
using Trinket.Api.Utilities;

namespace Trinket.Api.Manager
{
    public class VehicleManager : BaseManager
    {
        public Vehicle CreateOrUpdateVehicle(Vehicle vehicle)
        {
            var vehicleDetails = this.VehicleExternal.GetVehicleDetails(vehicle.LicensePlate);

            vehicle.YearModel = vehicleDetails.Brand;
            vehicle.City = vehicleDetails.City;
            vehicle.Color = vehicleDetails.Color;
            vehicle.LicensePlate = LicensePlateUtility.NormalizeLicensePlate(vehicleDetails.LicensePlate);
            vehicle.Model = vehicleDetails.Model;
            vehicle.Situation = vehicleDetails.Situation;
            vehicle.State = vehicleDetails.State;
            vehicle.Year = vehicleDetails.Year;
            vehicle.YearModel = vehicleDetails.YearModel;

            this.VehicleRepository.CreateOrUpdateVehicle(vehicle);

            return vehicle;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Trinket.Api.Models;

namespace Trinket.Api.Manager
{
    public class VehicleManager : BaseManager
    {
        public Vehicle CreateOrUpdateVehicle(Vehicle vehicle)
        {
            var vehicleDetails = this.VehicleExternal.GetVehicleDetails(vehicle.LicensePlate);

            //vehicle.
            return null;
        }
    }
}

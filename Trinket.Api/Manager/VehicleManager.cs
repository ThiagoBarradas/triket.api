using Nancy;
using System;
using System.Collections.Generic;
using System.Text;
using Trinket.Api.Models;
using Trinket.Api.Utilities;

namespace Trinket.Api.Manager
{
    public class VehicleManager : BaseManager
    {
        public BaseResponse<object> CreateOrUpdateVehicle(Vehicle vehicle)
        {
            BaseResponse<object> response = new BaseResponse<object>();

            var vehicleDetails = this.VehicleExternal.GetVehicleDetails(vehicle.LicensePlate);

            if (vehicleDetails == null || string.IsNullOrWhiteSpace(vehicleDetails.LicensePlate))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

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

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            response.SuccessBody = vehicle;

            return response;
        }

        public BaseResponse<object> Get(Vehicle vehicle)
        {
            BaseResponse<object> response = new BaseResponse<object>();

            var vehicleResult = this.VehicleRepository.GetVehicle(LicensePlateUtility.NormalizeLicensePlate(vehicle.LicensePlate));

            if (vehicleResult == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                response.IsSuccess = true;
                response.SuccessBody = vehicleResult;
                response.StatusCode = HttpStatusCode.OK;
            }

            return response;
        }
    }
}

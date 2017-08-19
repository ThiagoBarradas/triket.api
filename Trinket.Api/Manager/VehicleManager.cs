using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
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
            vehicle.OwnerId = vehicle.Owner.Id;

            this.OwnerRepository.CreateOrUpdateOwner(vehicle.Owner);
            this.VehicleRepository.CreateOrUpdateVehicle(vehicle);

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            response.SuccessBody = vehicle;

            return response;
        }

        public BaseResponse<object> GetVehicle(Vehicle vehicle)
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
                vehicleResult.Owner = this.OwnerRepository.GetOwner(vehicleResult.OwnerId);
                response.IsSuccess = true;
                response.SuccessBody = vehicleResult;
                response.StatusCode = HttpStatusCode.OK;
            }

            return response;
        }

        public BaseResponse<object> SearchVehicle(SearchVehicleRequest request)
        {
            BaseResponse<object> response = new BaseResponse<object>();

            var vehiclesResult = this.VehicleRepository.SearchVehicles(request.OwnerId);
            var ownerId = vehiclesResult.Items.FirstOrDefault()?.OwnerId;

            if (string.IsNullOrWhiteSpace(ownerId) == false)
            {
                var owner = this.OwnerRepository.GetOwner(ownerId);
                vehiclesResult.Items.ForEach(r => { r.Owner = owner; });
            }

            response.IsSuccess = true;
            response.SuccessBody = vehiclesResult;
            response.StatusCode = HttpStatusCode.OK;
        
            return response;
        }
    }
}

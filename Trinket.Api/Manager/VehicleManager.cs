using Nancy;
using System.Linq;
using Trinket.Api.Models;
using Trinket.Api.Utilities;

namespace Trinket.Api.Manager
{
    public class VehicleManager : BaseManager
    {
        public BaseResponse<object> CreateOrUpdateVehicle(Vehicle request)
        {
            BaseResponse<object> response = new BaseResponse<object>();

            var vehicleDetails = this.VehicleExternal.GetVehicleDetails(request.LicensePlate);

            if (vehicleDetails == null || string.IsNullOrWhiteSpace(vehicleDetails.LicensePlate))
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            request.YearModel = vehicleDetails.Brand;
            request.City = vehicleDetails.City;
            request.Color = vehicleDetails.Color;
            request.LicensePlate = LicensePlateUtility.NormalizeLicensePlate(vehicleDetails.LicensePlate);
            request.Model = vehicleDetails.Model;
            request.Situation = vehicleDetails.Situation;
            request.State = vehicleDetails.State;
            request.Year = vehicleDetails.Year;
            request.YearModel = vehicleDetails.YearModel;
            request.OwnerId = request.Owner.Id;

            this.OwnerRepository.CreateOrUpdateOwner(request.Owner);
            this.VehicleRepository.CreateOrUpdateVehicle(request);

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.SuccessBody = request;

            return response;
        }

        public BaseResponse<object> GetVehicle(Vehicle request)
        {
            BaseResponse<object> response = new BaseResponse<object>();

            var vehicleResult = this.VehicleRepository.GetVehicle(LicensePlateUtility.NormalizeLicensePlate(request.LicensePlate));

            if (vehicleResult == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            vehicleResult.Owner = this.OwnerRepository.GetOwner(vehicleResult.OwnerId);

            var ownerNotification = this.OwnerNotificationRepository.GetOwnerNotification(vehicleResult.OwnerId);
            if (ownerNotification != null && ownerNotification.OneSignalIds.Count > 0 && request.Owner.Id != vehicleResult.OwnerId)
            {
                this.OwnerNotificationExternal.SendPushNotification(vehicleResult, ownerNotification, vehicle, request.Owner);
            }

            
            response.IsSuccess = true;
            response.SuccessBody = vehicleResult;
            response.StatusCode = HttpStatusCode.OK;
        
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

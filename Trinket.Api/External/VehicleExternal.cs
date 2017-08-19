using RestSharp;
using Trinket.Api.Models;
using Trinket.Api.Utilities;

namespace Trinket.Api.External
{
    public class VehicleExternal
    {
        public VehicleDetailsExternal GetVehicleDetails(string licensePlate)
        {
            licensePlate = LicensePlateUtility.NormalizeLicensePlate(licensePlate);

            RestClient client = new RestClient("http://placa-wgenial.rhcloud.com/{licensePlate}");
            RestRequest request = new RestRequest(Method.GET);
            request.AddUrlSegment("licensePlate", licensePlate);

            var response = client.Execute<VehicleDetailsExternal>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }
    }
}

using Nancy;
using Trinket.Api.Manager;
using Trinket.Api.Models;

namespace Trinket.Api.Controller
{
    public abstract class BaseController : NancyModule
    {
        public BaseController()
        {
            this.VehicleManager = new VehicleManager();
        }

        public VehicleManager VehicleManager { get; set; }

        public object CreateResponse(BaseResponse<object> response)
        {
            Nancy.Response httpResponse = null;

            if (response.IsSuccess == true)
            {
                httpResponse = Response.AsJson(response.SuccessBody, response.StatusCode);
            }
            else
            {
                httpResponse = Response.AsJson(response.ErrorBody, response.StatusCode);
            }

            return httpResponse;
        }
    }
}

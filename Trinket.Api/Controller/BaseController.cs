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
            this.OwnerNotificationManager = new OwnerNotificationManager();
            this.VoiceManager = new VoiceManager();
        }

        public VehicleManager VehicleManager { get; set; }

        public VoiceManager VoiceManager { get; set; }

        public OwnerNotificationManager OwnerNotificationManager { get; set; }

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

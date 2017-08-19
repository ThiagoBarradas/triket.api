using Nancy;
using Nancy.ModelBinding;
using Trinket.Api.Models;

namespace Trinket.Api.Controller
{
    public class VehicleController : BaseController
    {
        public VehicleController()
        {
            this.Post("vehicles", args => this.CreateOrUpdate());
            this.Get("vehicles", args => this.Search());
        }

        public object Search()
        { 
            return Response.AsJson("");
        }

        public object CreateOrUpdate()
        {
            var request = this.Bind<Vehicle>();
            var response = this.VehicleManager.CreateOrUpdateVehicle(request);

            return Response.AsJson(response);
        }
    }
}

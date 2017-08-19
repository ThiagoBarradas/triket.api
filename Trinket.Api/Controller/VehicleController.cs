using Nancy;
using Nancy.ModelBinding;
using Trinket.Api.Models;

namespace Trinket.Api.Controller
{
    public class VehicleController : BaseController
    {
        public VehicleController()
        {
            this.Post("vehicles/register", args => this.CreateOrUpdate());
            this.Get("vehicles/search", args => this.Search());
            this.Post("vehicles/get", args => this.Get());
        }

        public object CreateOrUpdate()
        {
            var request = this.Bind<Vehicle>();
            var response = this.VehicleManager.CreateOrUpdateVehicle(request);

            return this.CreateResponse(response);
        }

        public object Get()
        {
            var request = this.Bind<Vehicle>();
            var response = this.VehicleManager.GetVehicle(request);

            return this.CreateResponse(response);
        }

        public object Search()
        {
            var request = this.Bind<SearchVehicleRequest>();
            var response = this.VehicleManager.SearchVehicle(request);

            return this.CreateResponse(response);
        }
    }
}

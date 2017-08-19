using Nancy;
using Nancy.ModelBinding;
using Trinket.Api.Models;

namespace Triket.Api.Controller
{
    public class VehicleController : NancyModule
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
            var response = new { vehicle = request };

            return Response.AsJson(response);
        }
    }
}

using Nancy;
using Trinket.Api.Manager;

namespace Trinket.Api.Controller
{
    public abstract class BaseController : NancyModule
    {
        public BaseController()
        {
            this.VehicleManager = new VehicleManager();
        }

        public VehicleManager VehicleManager { get; set; }
    }
}

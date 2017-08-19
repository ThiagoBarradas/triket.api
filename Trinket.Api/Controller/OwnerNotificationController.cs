using Nancy;
using Nancy.ModelBinding;
using Trinket.Api.Models;

namespace Trinket.Api.Controller
{
    public class OwnerNotificationController : BaseController
    {
        public OwnerNotificationController()
        {
            this.Post("notifications", args => this.CreateOrUpdate());
        }

        public object CreateOrUpdate()
        {
            var request = this.Bind<OwnerNotificationRequest>();
            var response = this.OwnerNotificationManager.CreateOrUpdateOwnerNotification(request);

            return this.CreateResponse(response);
        }
    }
}

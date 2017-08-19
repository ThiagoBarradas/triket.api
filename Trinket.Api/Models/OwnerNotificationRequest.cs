using System.Collections.Generic;

namespace Trinket.Api.Models
{
    public class OwnerNotificationRequest
    {
        public string OwnerId { get; set; }

        public string OneSignalId { get; set; }
    }
}

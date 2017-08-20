using System.Collections.Generic;

namespace Trinket.Api.Models
{
    public class OwnerNotificationRequest
    {
        public Owner Owner { get; set; }

        public string OneSignalId { get; set; }
    }
}

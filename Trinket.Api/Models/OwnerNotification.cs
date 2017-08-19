using System.Collections.Generic;

namespace Trinket.Api.Models
{
    public class OwnerNotification
    {
        public OwnerNotification()
        {
            this.OneSignalIds = new List<string>();
        }

        public string OwnerId { get; set; }

        public List<string> OneSignalIds { get; set; }
    }
}

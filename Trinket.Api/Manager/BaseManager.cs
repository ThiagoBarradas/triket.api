using System;
using System.Collections.Generic;
using System.Text;
using Trinket.Api.External;
using Trinket.Api.Repository;

namespace Trinket.Api.Manager
{
    public abstract class BaseManager
    {
        public BaseManager()
        {
            this.VehicleRepository = new VehicleRepository();
            this.OwnerRepository = new OwnerRepository();
            this.OwnerNotificationRepository = new OwnerNotificationRepository();
            this.VehicleExternal = new VehicleExternal();
            this.OwnerNotificationExternal = new OwnerNotificationExternal();
        }

        public VehicleRepository VehicleRepository { get; set; }

        public OwnerRepository OwnerRepository { get; set; }

        public OwnerNotificationRepository OwnerNotificationRepository { get; set; }

        public VehicleExternal VehicleExternal { get; set; }

        public OwnerNotificationExternal OwnerNotificationExternal { get; set; }

    }
}

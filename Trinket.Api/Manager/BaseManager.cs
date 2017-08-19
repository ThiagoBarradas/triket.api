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
        }

        public VehicleRepository VehicleRepository { get; set; }

        public VehicleExternal VehicleExternal { get; set; }
    }
}

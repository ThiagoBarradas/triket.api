using System;
using System.Collections.Generic;
using System.Text;

namespace Trinket.Api.Models
{
    public class Vehicle
    {
        public string LicensePlate { get; set; }

        public double[] Location { get; set; }

        public Owner Owner { get; set; }
    }
}

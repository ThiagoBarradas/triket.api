using System;
using System.Collections.Generic;
using System.Text;

namespace Trinket.Api.Models
{
    public class Vehicle
    {
        public string LicensePlate { get; set; }

        public string City {get; set;}

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }

        public bool IsStolen { get; set; }

        public double[] Location { get; set; }

        public Owner Owner { get; set; }
    }
}

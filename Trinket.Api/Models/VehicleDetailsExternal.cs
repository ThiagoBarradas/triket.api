using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Trinket.Api.Models
{
    public class VehicleDetailsExternal
    {
        [DeserializeAs(Name = "placa")]
        public string LicensePlate { get; set; }

        [DeserializeAs(Name = "modelo")]
        public string Model { get; set; }

        [DeserializeAs(Name = "marca")]
        public string Brand { get; set; }

        [DeserializeAs(Name = "ano")]
        public string Year { get; set; }

        [DeserializeAs(Name = "anoModelo")]
        public string YearModel { get; set; }

        [DeserializeAs(Name = "cor")]
        public string Color { get; set; }

        [DeserializeAs(Name = "situacao")]
        public string Situation { get; set; }

        [DeserializeAs(Name = "municipio")]
        public string City { get; set; }

        [DeserializeAs(Name = "uf")]
        public string State { get; set; }
    }
}

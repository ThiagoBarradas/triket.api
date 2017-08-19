using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Trinket.Api.Models
{
    public class VehicleDetailsExternal
    {
        [SerializeAs(Name = "placa")]
        public string LicensePlate { get; set; }

        [SerializeAs(Name = "modelo")]
        public string Model { get; set; }

        [SerializeAs(Name = "marca")]
        public string Brand { get; set; }

        [SerializeAs(Name = "ano")]
        public string Year { get; set; }

        [SerializeAs(Name = "anoModelo")]
        public string YearModel { get; set; }

        [SerializeAs(Name = "cor")]
        public string Color { get; set; }

        [SerializeAs(Name = "situacao")]
        public string Situation { get; set; }

        [SerializeAs(Name = "municipio")]
        public string City { get; set; }

        [SerializeAs(Name = "uf")]
        public string State { get; set; }
    }
}

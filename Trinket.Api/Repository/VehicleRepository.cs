using Elasticsearch.Net;
using Nest;
using System.Linq;
using Trinket.Api.Models;
using Trinket.Api.Repository.QueryBuilders;

namespace Trinket.Api.Repository
{
    public class VehicleRepository : BaseRepository
    {
        public const string INDEX_NAME = "vehicle";
        public const string TYPE_NAME = "vehicle";

        public VehicleRepository() : base(INDEX_NAME) { }

        public bool CreateOrUpdateVehicle(Vehicle vehicle)
        {
            var result = this.ElasticClient.Index<Vehicle>(vehicle, q =>
              q.Type(TYPE_NAME)
               .Id(vehicle.LicensePlate)
               .Refresh(Refresh.True));

            if (result.OriginalException != null) throw result.OriginalException;

            return result.Created;
        }

        public Vehicle GetVehicle(string licensePlate)
        {
            var result = this.ElasticClient.Get<Vehicle>(licensePlate, idx => idx.Type(TYPE_NAME));

            if (result.OriginalException != null && result.IsValid == false) throw result.OriginalException;

            return result.Source;
        }

        public SearchContainer<Vehicle> SearchVehicles(string ownerId)
        {
            var descriptor = new SearchDescriptor<Vehicle>();
            descriptor.Type(TYPE_NAME);
            descriptor.Size(999);
            descriptor.From(0);

            var container = QueryBuilder.CreateMatchQuery("ownerId", ownerId);
            descriptor.ApplyQuery(container);

            var result = this.ElasticClient.Search<Vehicle>(descriptor);

            if (result.OriginalException != null) throw result.OriginalException;

            return new SearchContainer<Vehicle>()
            {
                Items = result.Documents.ToList(),
                Total = result.Total
            };
        }
    }
}

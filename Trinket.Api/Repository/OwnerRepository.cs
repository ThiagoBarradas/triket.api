using Elasticsearch.Net;
using Nest;
using System.Linq;
using Trinket.Api.Models;
using Trinket.Api.Repository.QueryBuilders;

namespace Trinket.Api.Repository
{
    public class OwnerRepository : BaseRepository
    {
        public const string INDEX_NAME = "owner";
        public const string TYPE_NAME = "owner";

        public OwnerRepository() : base(INDEX_NAME) { }

        public bool CreateOrUpdateOwner(Owner Owner)
        {
            var result = this.ElasticClient.Index<Owner>(Owner, q =>
              q.Type(TYPE_NAME)
               .Id(Owner.Id)
               .Refresh(Refresh.True));

            if (result.OriginalException != null) throw result.OriginalException;

            return result.Created;
        }

        public Owner GetOwner(string id)
        {
            var result = this.ElasticClient.Get<Owner>(id, idx => idx.Type(TYPE_NAME));

            if (result.OriginalException != null && result.IsValid == false) throw result.OriginalException;

            return result.Source;
        }
    }
}

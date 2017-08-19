using Elasticsearch.Net;
using Nest;
using System.Linq;
using Trinket.Api.Models;
using Trinket.Api.Repository.QueryBuilders;

namespace Trinket.Api.Repository
{
    public class OwnerNotificationRepository : BaseRepository
    {
        public const string INDEX_NAME = "owner-notification";
        public const string TYPE_NAME = "owner-notification";

        public OwnerNotificationRepository() : base(INDEX_NAME) { }

        public bool CreateOrUpdateOwnerNotification(OwnerNotification ownerNotification)
        {
            var result = this.ElasticClient.Index<OwnerNotification>(ownerNotification, q =>
              q.Type(TYPE_NAME)
               .Id(ownerNotification.OwnerId)
               .Refresh(Refresh.True));

            if (result.OriginalException != null) throw result.OriginalException;

            return result.Created;
        }

        public OwnerNotification GetOwnerNotification(string ownerId)
        {
            var result = this.ElasticClient.Get<OwnerNotification>(ownerId, idx => idx.Type(TYPE_NAME));

            if (result.OriginalException != null && result.IsValid == false) throw result.OriginalException;

            return result.Source;
        }
    }
}

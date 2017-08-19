using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Trinket.Api.Repository
{
    public abstract class BaseRepository
    {
        protected IElasticClient ElasticClient { get; set; }

        public BaseRepository(string index = null)
        {
            this.ElasticClient = this.ClientFactory(index);
        }

        private IElasticClient ClientFactory(string index)
        {
            var uris = new List<Uri>();
            uris.Add(new Uri("http://barradas.eastus2.cloudapp.azure.com:9400"));

            var connectionPool = new StaticConnectionPool(uris);
            var connectionSettings = new ConnectionSettings(connectionPool);
            connectionSettings.BasicAuthentication("elastic","SuckMy#471");

            if (string.IsNullOrWhiteSpace(index) == false)
            {
                connectionSettings.DefaultIndex(index);
            }

            var elasticClient = new ElasticClient(connectionSettings);

            return elasticClient;
        }  
    }
}

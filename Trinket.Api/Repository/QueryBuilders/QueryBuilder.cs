using Nest;
using System.Collections.Generic;
using System.Linq;

namespace Trinket.Api.Repository.QueryBuilders
{
    public static class QueryBuilder
    {
        public static QueryContainer QueryJoin(this QueryContainer container, QueryContainer queryToAdd, Operator filterOperator = Operator.And)
        {
            if (queryToAdd == null) return container;

            switch (filterOperator)
            {
                case Operator.And:
                    container = (container == null) ? queryToAdd : container && queryToAdd;
                    break;
                case Operator.Or:
                    container = (container == null) ? queryToAdd : container || queryToAdd;
                    break;
            }

            return container;
        }

        public static QueryContainer CreateMatchQuery(string field, string value, Operator queryOperator = Operator.And, bool contains = true)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            QueryContainer matchQuery = new MatchQuery()
            {
                Field = field,
                Query = value,
                Operator = queryOperator
            };

            return (contains == false) ? CreateNotQuery(matchQuery) : matchQuery;
        }

        public static QueryContainer CreateTermQuery(string field, object value, bool contains = true)
        {
            if (value == null) return null;

            QueryContainer termQuery = new TermQuery()
            {
                Field = field,
                Value = value
            };

            return (contains == false) ? CreateNotQuery(termQuery) : termQuery;
        }

        public static QueryContainer CreateTermsQuery(string field, IEnumerable<object> values, bool contains = true)
        {
            if (values == null || values.Any() == false) return null;

            QueryContainer termsQuery = new TermsQuery()
            {
                Field = field,
                Terms = values
            };

            return (contains == false) ? CreateNotQuery(termsQuery) : termsQuery;
        }

        public static QueryContainer CreateNotQuery(QueryContainer query)
        {
            if (query == null) return null;

            List<QueryContainer> querys = new List<QueryContainer>();
            querys.Add(query);

            QueryContainer notTermQuery = new BoolQuery()
            {
                MustNot = querys
            };
            return notTermQuery;
        }

        public static SearchDescriptor<T> ApplyQuery<T>(this SearchDescriptor<T> descriptor, QueryContainer query) where T : class
        {
            if (query == null) return descriptor;
            return descriptor.Query(q => query);
        }

        public static SearchDescriptor<T> ApplySorting<T>(this SearchDescriptor<T> descriptor, string field, object mode) where T : class
        {
            if (string.IsNullOrWhiteSpace(field)) return descriptor;

            var lowerMode = mode.ToString().ToLowerInvariant();
            SortOrder sortOrder = (lowerMode == "desc" || lowerMode == "descending") ? SortOrder.Descending : SortOrder.Ascending;
            var fieldObj = new Field(field);

            return descriptor.Sort(s => s
                .Field(f => f.Field(fieldObj)
                .Order(sortOrder)));
        }
    }
}

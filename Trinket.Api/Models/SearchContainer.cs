using System.Collections.Generic;

namespace Trinket.Api.Models
{
    public class SearchContainer<T>
    {
        public long Total { get; set; }

        public List<T> Items { get; set; }
    }
}

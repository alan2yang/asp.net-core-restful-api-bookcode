using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library.API.Models
{
    public abstract class Resource
    {
        [JsonProperty("_links", Order = 100)]
        public List<Link> Links { get; } = new List<Link>();
    }

    public class ResourceCollection<T> : Resource
        where T : Resource
    {
        public ResourceCollection(List<T> items)
        {
            Items = items;
        }

        public List<T> Items { get; }
    }
}
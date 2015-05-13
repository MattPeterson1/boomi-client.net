using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class QueryResult<T>
    {
        [JsonProperty(PropertyName = "queryToken")]
        public string QueryToken;
        
        [JsonProperty(PropertyName = "result")] 
        public List<T> Results;

        [JsonProperty(PropertyName = "numberOfResults")] 
        public int NumberOfResults;
    }
}

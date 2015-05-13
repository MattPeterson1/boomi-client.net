using System;
using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class Environment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "classification")]
        public string Classification { get; set; }
    }
}

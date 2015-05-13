using System;
using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class AccountGroup
    {
        [JsonProperty( PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty( PropertyName = "accountId")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "autoSubscribeAlertLevel")]
        public string AutoSubscribeAlertLevel { get; set; }

        [JsonProperty(PropertyName = "defaultGroup")]
        public Boolean IsDefaultGroup { get; set; }
    }
}

using System;
using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class Account
    {
        [JsonProperty( PropertyName = "accountId")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime? DateCreated { get; set; }
        
        [JsonProperty(PropertyName = "expirationDate")]
        public DateTime? ExpirationDate { get; set; }

        [JsonProperty(PropertyName = "widgetAccount")]
        public Boolean WidgetAccount { get; set; }

        [JsonProperty(PropertyName = "suggestionsEnabled")]
        public Boolean SuggestionsEnabled { get; set; }

        [JsonProperty(PropertyName = "supportAccess")]
        public Boolean SupportAccess { get; set; }

        [JsonProperty(PropertyName = "supportLevel")]
        public string SupportLevel { get; set; }
    }
}

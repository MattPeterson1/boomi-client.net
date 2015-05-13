using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class Error
    {
        [JsonProperty(PropertyName = "message")]
        public string Message;
    }
}

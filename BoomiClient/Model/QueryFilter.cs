using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class QueryFilter
    {
        [JsonProperty(PropertyName = "expression")] 
        public CompoundExpression Expression;
    }
}

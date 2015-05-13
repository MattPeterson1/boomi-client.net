using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class Expression
    {
        [JsonProperty(PropertyName = "argument")]
        public List<string> Arguments;

        [JsonProperty(PropertyName = "operator")]
        public ExpressionOperator Operator;

        [JsonProperty(PropertyName = "property")]
        public string Property;

        public enum ExpressionOperator
        {
            // ReSharper disable InconsistentNaming
            EQUALS, 
            NOT_EQUALS,
            IS_NULL,
            IS_NOT_NULL,
            GREATER_THAN,
            GREATER_THAN_OR_EQUAL,
            LESS_THAN,
            LESS_THAN_OR_EQUAL
            // ReSharper restore InconsistentNaming
        }
    }
}


using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dell.Boomi.Client.Model
{
    public class CompoundExpression
    {
        [JsonProperty(PropertyName = "operator")]
        public CompoundExpressionOperator? Operator;

        [JsonProperty(PropertyName = "nestedExpression")] 
        public List<Expression> NestedExpressions;

        public enum CompoundExpressionOperator
        {
            // ReSharper disable InconsistentNaming
            AND,
            OR
            // ReSharper restore InconsistentNaming
        }
    }
}

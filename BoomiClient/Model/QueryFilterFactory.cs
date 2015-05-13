using System.Collections.Generic;

namespace Dell.Boomi.Client.Model
{
    // Someday, it might make sense to write a real builder.  For now this simple factory should make for easy creation
    // of QueryFilter for the most common scenarios
    public class QueryFilterFactory
    {
        public static QueryFilter CreateSimpleQueryFilter(Expression expression)
        {
            return new QueryFilter
            {
                Expression = new CompoundExpression {NestedExpressions = new List<Expression> {expression}}
            };
        }

        public static QueryFilter CreateAndQueryFilter(List<Expression> expressions)
        {
            return new QueryFilter
            {
                Expression = new CompoundExpression { Operator = CompoundExpression.CompoundExpressionOperator.AND, NestedExpressions = expressions }
            };
        }

        public static QueryFilter CreateOrQueryFilter(List<Expression> expressions)
        {
            return new QueryFilter
            {
                Expression = new CompoundExpression { Operator = CompoundExpression.CompoundExpressionOperator.OR, NestedExpressions = expressions }
            };
        }

    }
}

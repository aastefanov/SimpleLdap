using System.Linq.Expressions;

namespace SimpleLdap.Helpers
{
    public static class Expressions
    {
        public static Expression StripQuotes(this Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression) e).Operand;
            }

            return e;
        }
    }
}
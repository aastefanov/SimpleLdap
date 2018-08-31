using System;
using System.Linq.Expressions;

namespace SimpleLdap.Expressions.Visitors
{
    public class BooleanRewriter : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression node)
        {
            Type type = node.Member.DeclaringType;
            if (type != null &&
                type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                node.Member.Name == "HasValue")
                return Expression.Constant(true);

            return base.VisitMember(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            var value = (bool) Expression.Lambda(node.Test).Compile().DynamicInvoke();
            return value ? node.IfTrue : node.IfFalse;
        }
    }
}
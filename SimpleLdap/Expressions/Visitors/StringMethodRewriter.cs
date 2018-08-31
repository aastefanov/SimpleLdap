using System.Linq.Expressions;
using System.Reflection;
using SimpleLdap.Helpers;

namespace SimpleLdap.Expressions.Visitors
{
    public class StringMethodRewriter : ExpressionVisitor
    {
        private static class StringMethods
        {
            public static readonly MethodInfo Contains =
                typeof(string).GetMethod(nameof(string.Contains), new[] {typeof(string)});
            public static readonly MethodInfo StartsWith =
                typeof(string).GetMethod(nameof(string.StartsWith), new[] {typeof(string)});
            public static readonly MethodInfo EndsWith =
                typeof(string).GetMethod(nameof(string.EndsWith), new[] {typeof(string)});
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            string parameter = (node.Arguments[0].StripQuotes() as ConstantExpression)?.Value.ToString();


            if (TryCreateQueryString(parameter, node.Method, out string result))
                return Expression.MakeBinary(ExpressionType.Equal, node.Object,
                    Expression.Constant(result));
            
            
            return base.VisitMethodCall(node);
        }

        private static bool TryCreateQueryString(string parameter, MethodInfo method, out string result)
        {
            if (method == StringMethods.Contains) result = $"*{parameter}*";
            else if (method == StringMethods.StartsWith) result = $"{parameter}*";
            else if (method == StringMethods.EndsWith) result = $"*{parameter}";
            else result = "";

            return !string.IsNullOrEmpty(result);
        }
    }
}
using System;
using System.Linq.Expressions;
using System.Reflection;
using SimpleLdap.Attributes;
using SimpleLdap.Helpers;

namespace SimpleLdap.Expressions
{
    public class FilterBuilder
    {
        private readonly LdapAttributeMapper _mapper;

        public FilterBuilder(LdapAttributeMapper mapper)
        {
            _mapper = mapper;
        }

        public string Build(Expression expression)
        {
            if (expression is BinaryExpression expr) return $"{GetChildren(expr)}";
            throw new ArgumentException("Expression is not BinaryExpression", nameof(expression));
        }

        private string GetChildren(BinaryExpression expression)
        {
            if (expression == null) return string.Empty;
            Expression left = expression.Left;
            Expression right = expression.Right;

            bool isLeftNested = left is BinaryExpression;
            bool isRightNested = right is BinaryExpression;

            if (isLeftNested && isRightNested)
            {
                string leftEval = GetChildren(left as BinaryExpression);
                string rightEval = GetChildren(right as BinaryExpression);

                return $"({expression.NodeType.ToLdapOperator()}{leftEval}{rightEval})";
            }

            if (isLeftNested || isRightNested)
                throw new ArgumentException($"Cannot put constant in expression {expression}");
            MemberExpression property = left as MemberExpression ?? right as MemberExpression;
            ConstantExpression constant = left as ConstantExpression ?? right as ConstantExpression;


            if (property == null || constant == null)
                throw new ArgumentException($"Cannot evaluate expression {expression}");
            string propertyRealName =
                _mapper.GetAttributeKey(property.Member.GetCustomAttribute<LdapAttributeAttribute>().Attribute);

            return expression.NodeType != ExpressionType.NotEqual
                ? $"({propertyRealName}{expression.NodeType.ToLdapOperator()}{constant.Value})"
                : $"!({propertyRealName}={constant.Value})";
        }
    }
}
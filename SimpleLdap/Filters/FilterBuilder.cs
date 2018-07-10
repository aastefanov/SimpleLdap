using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleLdap.Attributes;
using SimpleLdap.Helpers;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Filters
{
    public class FilterBuilder<TProvider> where TProvider : ILdapProvider
    {
        private readonly LdapAttributeMapper<TProvider> _mapper;

        public FilterBuilder(LdapAttributeMapper<TProvider> mapper)
        {
            _mapper = mapper;
        }

        public string Build<T1>(Expression<Func<T1, bool>> expression)
        {
            if (expression.Body is BinaryExpression body) return $"{GetChildren(body)}";
            throw new ArgumentException("Expression is not BinaryExpression", nameof(expression));
        }

        private string GetChildren(BinaryExpression expression)
        {
            if (expression == null) return string.Empty;
            var left = expression.Left;
            var right = expression.Right;

            var isLeftNested = left is BinaryExpression;
            var isRightNested = right is BinaryExpression;

            if (isLeftNested && isRightNested)
            {
                var leftEval = GetChildren(left as BinaryExpression);
                var rightEval = GetChildren(right as BinaryExpression);

                return $"({expression.NodeType.ToLdapOperator()}{leftEval}{rightEval})";
            }

            if (isLeftNested || isRightNested)
                throw new ArgumentException($"Cannot put constant in expression {expression}");
            var property = left as MemberExpression ?? right as MemberExpression;
            var constant = left as ConstantExpression ?? right as ConstantExpression;


            if (property == null || constant == null)
                throw new ArgumentException($"Cannot evaluate expression {expression}");
            var propertyRealName =
                _mapper.GetAttributeKey(property.Member.GetCustomAttribute<LdapAttributeAttribute>().Attribute);

            if (expression.NodeType != ExpressionType.NotEqual)
            {
                return $"({propertyRealName}{expression.NodeType.ToLdapOperator()}{constant.Value})";
            }

            return
                $"{ExpressionType.Not.ToLdapOperator()}" +
                $"({propertyRealName}{ExpressionType.Equal.ToLdapOperator()}{constant.Value})";
        }
    }
}
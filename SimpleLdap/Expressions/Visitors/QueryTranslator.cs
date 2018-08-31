using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SimpleLdap.Attributes;
using SimpleLdap.Helpers;

namespace SimpleLdap.Expressions.Visitors
{
    public class QueryTranslator : ExpressionVisitor
    {
        private readonly LdapAttributeMapper _mapper;
        private StringBuilder _sb;

        public QueryTranslator(LdapAttributeMapper mapper)
        {
            _mapper = mapper;
        }

        public string Translate(Expression expression)
        {
            _sb = new StringBuilder();
            Visit(expression);

            return _sb.ToString();
        }


        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable))
            {
                VisitQueryableMethods(m);
            }
            else if (m.Method.DeclaringType == typeof(string))
            {
                VisitStringMethods(m);
            }

            else return Expression.Constant(Expression.Lambda(m).Compile().DynamicInvoke());

            return m;
        }

        private void VisitQueryableMethods(MethodCallExpression m)
        {
            foreach (Expression arg in m.Arguments) Visit(arg);
        }

        private void VisitStringMethods(Expression m)
        {
            var rewriter = new StringMethodRewriter();

            Visit(rewriter.Visit(m));
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    Visit(u.Operand);
                    break;
                case ExpressionType.Quote:
                    Visit(u.StripQuotes());
                    break;
                case ExpressionType.Convert:
                    Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException($"The unary operator '{u.NodeType}' is not supported");
            }

            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            _sb.Append(new FilterBuilder(_mapper).Build(b));
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c.Value is Expression expression) Visit(expression);

            return c;
        }

        private string GetMemberName(MemberExpression m)
        {
            return _mapper.GetAttributeKey(m.Member.GetCustomAttribute<LdapAttributeAttribute>());
        }

        public string Translate<T>(Expression<Func<T, bool>> expression)
        {
            return Translate(expression as Expression);
        }
    }
}
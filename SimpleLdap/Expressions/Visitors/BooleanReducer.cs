using System.Linq.Expressions;

namespace SimpleLdap.Expressions.Visitors
{
    public class BooleanReducer : ExpressionVisitor
    {
        private bool _requiresEvaluation = true;

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Left is ConstantExpression constantLeft && constantLeft.Value is bool isLeftTrue)
            {
                _requiresEvaluation = true;
                switch (node.NodeType)
                {
                    case ExpressionType.AndAlso:
                        return Visit(isLeftTrue ? node.Right : node.Left);
                    case ExpressionType.OrElse:
                        return Visit(isLeftTrue ? node.Left : node.Right);
                }
            }

            if (node.Right is ConstantExpression constantRight && constantRight.Value is bool isRightTrue)
            {
                _requiresEvaluation = true;
                switch (node.NodeType)
                {
                    case ExpressionType.AndAlso:
                        return Visit(isRightTrue ? node.Left : node.Right);
                    case ExpressionType.OrElse:
                        return Visit(isRightTrue ? node.Right : node.Left);
                }
            }

            return base.VisitBinary(node);
        }

        public Expression Reduce(Expression expression)
        {
            Expression reduced = expression;
            while (_requiresEvaluation)
            {
                _requiresEvaluation = false;
                reduced = Visit(reduced);
            }

            return reduced;
        }
    }
}
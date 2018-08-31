using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using NUnit.Framework;
using SimpleLdap.Expressions.Visitors;
using SimpleLdap.Tests.Models;

#pragma warning disable 162

namespace SimpleLdap.Tests.Expressions.Visitors
{
    [TestFixture]
    [SuppressMessage("ReSharper", "RedundantLogicalConditionalExpressionOperand")]
    [SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
    public class BooleanReducerTests
    {
        [Test]
        public void Reduce_Duplicate_True_RemovesDuplicates()
        {
            Expression<Func<bool, bool>> input = e => true && true;
            Expression<Func<bool, bool>> expected = e => true;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_BoolAtFrontAndEnd_RemovesDuplicates()
        {
            Expression<Func<LdapUser, bool>> input = e =>
                true && true && e.CommonName == "gosho" && true;

            Expression<Func<LdapUser, bool>> expected = e => e.CommonName == "gosho";

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }


        [Test]
        public void Reduce_BoolAtEnd_RemovesDuplicates()
        {
            Expression<Func<LdapUser, bool>> input = e => e.CommonName == "gosho" && true && true;

            Expression<Func<LdapUser, bool>> expected = e => e.CommonName == "gosho";

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_MultipleTrue_ReturnsTrue()
        {
            Expression<Func<LdapUser, bool>> input = e => true && true && true && true;
            Expression<Func<LdapUser, bool>> expected = e => true;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_MultipleOrElseTrueAtFront_ReturnsTrue()
        {
            Expression<Func<LdapUser, bool>> input = e => true || false || false || true || true;
            Expression<Func<LdapUser, bool>> expected = e => true;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_MultipleOrElseFalseAtFront_ReturnsTrue()
        {
            Expression<Func<LdapUser, bool>> input = e => false || false || false || false || true;
            Expression<Func<LdapUser, bool>> expected = e => true;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_MultipleAndAlsoTrueAndNestedAndAlso_ReturnsTrue()
        {
            Expression<Func<LdapUser, bool>> input = e => true && true && (true && true);
            Expression<Func<LdapUser, bool>> expected = e => true;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_MultipleAndAlsoTrueAndNestedFalseAndAlso_ReturnsFalse()
        {
            Expression<Func<LdapUser, bool>> input = e => true && true && (false && true);
            Expression<Func<LdapUser, bool>> expected = e => false;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_MultipleAndAlsoTrueAndMultipleAndAlsoFalse_ReturnsFalse()
        {
            Expression<Func<LdapUser, bool>> input = e =>
                true && true && false && false && true && true && false;
            Expression<Func<LdapUser, bool>> expected = e => false;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_MultipleAndAlsoFalse_ReturnsFalse()
        {
            Expression<Func<LdapUser, bool>> input = e => false && false && false && false;
            Expression<Func<LdapUser, bool>> expected = e => false;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_BoolAtFrontAndEndWithFalseInMiddle_ReturnsFalse()
        {
            Expression<Func<LdapUser, bool>> input = e =>
                true && e.CommonName == "gosho" && false && true;
            Expression<Func<LdapUser, bool>> expected = e => false;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_OrElseWithLeftTrue_ReturnsTrue()
        {
            Expression<Func<LdapUser, bool>> input = e => true || e.CommonName == "gosho";
            Expression<Func<LdapUser, bool>> expected = e => true;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_OrElseWithTrueAtEnd_RemovesRedundantTrue()
        {
            Expression<Func<LdapUser, bool>> input = e => e.CommonName == "gosho" || true;
            Expression<Func<LdapUser, bool>> expected = e => true;

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_ComplexAndAlsoWithOrElseAtEnd_ReturnsCorrectExpression()
        {
            Expression<Func<LdapUser, bool>> input = e =>
                true && e.CommonName == "gosho" && (true || e.CommonName == "gosho");
            Expression<Func<LdapUser, bool>> expected = e => e.CommonName == "gosho";

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_ComplexAndAlsoWithOrElseAtFront_ReturnsCorrectExpression()
        {
            Expression<Func<LdapUser, bool>> input = e =>
                (true || e.CommonName == "gosho") && true && e.CommonName == "gosho";
            Expression<Func<LdapUser, bool>> expected = e => e.CommonName == "gosho";

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }

        [Test]
        public void Reduce_ComplexAndAlsoWithIrreducableOrElseAtFront_ReturnsCorrectExpression()
        {
            Expression<Func<LdapUser, bool>> input = e =>
                (false || e.CommonName == "gosho") && true && e.CommonName == "gosho";
            Expression<Func<LdapUser, bool>> expected = e =>
                e.CommonName == "gosho" && e.CommonName == "gosho";

            Expression rewritten = new BooleanReducer().Reduce(input);

            Assert.AreEqual(expected.ToString(), rewritten.ToString());
        }
    }
}
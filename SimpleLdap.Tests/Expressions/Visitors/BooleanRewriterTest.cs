using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using NUnit.Framework;
using SimpleLdap.Expressions.Visitors;
using SimpleLdap.Tests.Models;

namespace SimpleLdap.Tests.Expressions.Visitors
{
    [TestFixture]
    [SuppressMessage("ReSharper", "RedundantLogicalConditionalExpressionOperand")]
    [SuppressMessage("ReSharper", "RedundantBoolCompare")]
    public class BooleanRewriterTest
    {
        [Test]
        public void Rewrite_NullableWithHasValueAndPropertyEquality_RewritesHasValue()
        {
            Expression<Func<LdapGroup, bool>> input = e => e.PhoneNumber.HasValue && e.PhoneNumber == 1234567890;
            // ReSharper disable once RedundantLogicalConditionalExpressionOperand
            Expression<Func<LdapGroup, bool>> expected = e => true && e.PhoneNumber == 1234567890;

            Expression rewritten = new BooleanRewriter().Visit(input);

            Assert.AreEqual(expected.ToString(), rewritten?.ToString());
        }

        [Test]
        public void Rewrite_Conditional_RewritesCorrectly()
        {
            Expression<Func<LdapGroup, bool>> input = e =>
                ("test".Equals("test")
                    ? e.DistinguishedName == "cn=fakegroup,dc=example,dc=com"
                    : e.PhoneNumber == 1234567890);
            Expression<Func<LdapGroup, bool>> expected = e => e.DistinguishedName == "cn=fakegroup,dc=example,dc=com";

            Expression rewritten = new BooleanRewriter().Visit(input);

            Assert.AreEqual(expected.ToString(), rewritten?.ToString());
        }
    }
}
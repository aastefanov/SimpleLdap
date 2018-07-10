using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NSubstitute;
using NUnit.Framework;
using SimpleLdap.Attributes;
using SimpleLdap.Filters;
using SimpleLdap.Helpers;
using SimpleLdap.Interfaces;
using SimpleLdap.Tests.Models;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class LdapFilterBuilderTests
    {
        private FilterBuilder<ILdapProvider> CreateBuilder()
        {
            var provider = Substitute.For<ILdapProvider>();
            provider.AttributeNames
                .Returns(new Dictionary<LdapAttribute, string>
                {
                    {LdapAttribute.DistinguishedName, "dn"},
                    {LdapAttribute.FirstName, "gn"},
                    {LdapAttribute.FullName, "cn"}
                });

            var mapper = new LdapAttributeMapper<ILdapProvider>(provider);

            return new FilterBuilder<ILdapProvider>(mapper);
        }

        [Test]
        public void LdapFilter_NestedExpression_Value()
        {
            var builder = CreateBuilder();

            var result =
                builder.Build<LdapUser>(x =>
                    (x.CommonName == "gosho" || x.DistinguishedName == "pesho") && x.GivenName == "sasho" ||
                    x.CommonName == "tisho");


            Assert.AreEqual("(|(&(|(cn=gosho)(dn=pesho))(gn=sasho))(cn=tisho))", result);
        }

        [Test]
        public void LdapFilter_SimpleExpression_Value()
        {
            var builder = CreateBuilder();

            var result =
                builder.Build<LdapUser>(x => x.CommonName == "gosho");


            Assert.AreEqual("(cn=gosho)", result);
        }

        [Test]
        public void LdapFilter_NegateExpression_Value()
        {
            var builder = CreateBuilder();

            var result =
                builder.Build<LdapUser>(x => x.CommonName != "gosho");


            Assert.AreEqual("!(cn=gosho)", result);
        }

        [Test]
        public void LdapFilter_InvalidExpression_Throws()
        {
            var builder = CreateBuilder();

            void Result() => builder.Build<LdapUser>(x => true);

            Assert.That(Result, Throws.ArgumentException.With.Message, "Expression is not BinaryExpression");
        }
    }
}
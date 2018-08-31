using NUnit.Framework;
using SimpleLdap.Expressions.Visitors;
using SimpleLdap.Tests.Helpers;
using SimpleLdap.Tests.Models;

namespace SimpleLdap.Tests.Expressions.Translators
{
    public class ExpressionTranslationTests
    {
        private QueryTranslator _translator;

        [SetUp]
        public void SetUp() => _translator = Generators.CreateTranslator();

        [Test]
        public void LdapFilter_NestedExpression_Value()
        {
            string result =
                _translator.Translate<LdapUser>(x =>
                    (x.CommonName == "gosho" || x.DistinguishedName == "pesho") && x.GivenName == "sasho" ||
                    x.CommonName == "tisho");


            Assert.AreEqual("(|(&(|(cn=gosho)(dn=pesho))(gn=sasho))(cn=tisho))", result);
        }

        [Test]
        public void LdapFilter_SimpleExpression_Value()
        {
            string result =
                _translator.Translate<LdapUser>(x => x.CommonName == "gosho");


            Assert.AreEqual("(cn=gosho)", result);
        }

        [Test]
        public void LdapFilter_NegateExpression_Value()
        {
            string result =
                _translator.Translate<LdapUser>(x => x.CommonName != "gosho");


            Assert.AreEqual("!(cn=gosho)", result);
        }

        [Test]
        public void LdapFilter_InvalidExpression_Throws()
        {
            string expression = _translator.Translate<LdapUser>(x => true);

            Assert.AreEqual(string.Empty, expression);
        }
    }
}
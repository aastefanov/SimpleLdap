using NUnit.Framework;
using SimpleLdap.Expressions.Visitors;
using SimpleLdap.Tests.Helpers;
using SimpleLdap.Tests.Models;

namespace SimpleLdap.Tests.Expressions.Translators
{
    [TestFixture]
    public class StringTranslationTests
    {
        private QueryTranslator _translator;

        [SetUp]
        public void Setup() => _translator = Generators.CreateTranslator();

        [Test]
        public void LdapFilter_StartsWith_Value()
        {
            string result = _translator.Translate<LdapUser>(x => x.CommonName.StartsWith("gosho"));

            Assert.AreEqual("(cn=gosho*)", result);
        }

        [Test]
        public void LdapFilter_EndsWith_Value()
        {
            string result = _translator.Translate<LdapUser>(x => x.CommonName.EndsWith("gosho"));

            Assert.AreEqual("(cn=*gosho)", result);
        }

        [Test]
        public void LdapFilter_Contains_Value()
        {
            string result = _translator.Translate<LdapUser>(x => x.CommonName.Contains("gosho"));

            Assert.AreEqual("(cn=*gosho*)", result);
        }
    }
}
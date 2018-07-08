using NUnit.Framework;
using SimpleLdap.Attributes;
using SimpleLdap.Providers;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class LdapMapperUnitTests
    {
        [Test]
        public void AttributeMapper_GivenName_GetValue()
        {
            var mapperAd =
                new LdapAttributeMapper<ActiveDirectoryLdapProvider>();

            var mapperOpenLdap =
                new LdapAttributeMapper<OpenLdapProvider>();


            Assert.Multiple(() =>
                {
                    Assert.AreEqual("givenName", mapperAd.GetAttributeKey(LdapAttribute.FirstName));
                    Assert.AreEqual("givenName", mapperOpenLdap.GetAttributeKey(LdapAttribute.FirstName));
                }
            );
        }
    }
}
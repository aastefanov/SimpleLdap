using NUnit.Framework;
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
                    Assert.AreEqual("sn", mapperAd.Map(LdapAttribute.GivenName));
                    Assert.AreEqual("givenName", mapperOpenLdap.Map(LdapAttribute.GivenName));
                }
            );
        }
    }
}
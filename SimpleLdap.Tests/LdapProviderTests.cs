using NUnit.Framework;
using SimpleLdap.Providers;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class LdapProviderTests
    {
        [Test]
        public void Provider_Dn_HasValue()
        {
            var provider = new ActiveDirectoryLdapProvider();

            Assert.AreEqual("DN", provider.AttributeNames[LdapAttribute.DistinguishedName]);
        }
    }
}
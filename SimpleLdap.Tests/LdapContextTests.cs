using LinqToLdap;
using NUnit.Framework;
using SimpleLdap.Providers;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class LdapServiceTests
    {
        [Test]
        public void ProviderMapped_HasValue()
        {
            var service = new LdapContext<ActiveDirectoryLdapProvider>(new LdapConfiguration());
        }
    }
}
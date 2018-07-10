using System.Linq;
using NUnit.Framework;
using SimpleLdap.Attributes;
using SimpleLdap.Helpers;
using SimpleLdap.Tests.Models;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class LdapEntityUnitTests
    {
        [Test]
        public void DirectoryAttributes_Count()
        {
            var user = CreateUser();

//            var attributes = user.GetAttributes<LdapAttributeAttribute>().ToList();

//            Assert.That(attributes, Has.Count.EqualTo(3));
        }

        private static LdapUser CreateUser() => new LdapUser
        {
            DistinguishedName = "gosho",
            GivenName = "pesho",
            CommonName = "sasho"
        };
    }
}
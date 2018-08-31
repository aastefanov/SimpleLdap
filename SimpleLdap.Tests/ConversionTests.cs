using Novell.Directory.Ldap;
using NUnit.Framework;
using SimpleLdap.Helpers;
using SimpleLdap.Interfaces;
using SimpleLdap.Tests.Helpers;
using SimpleLdap.Tests.Models;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class ConversionTests
    {
        private LdapAttributeMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = Generators.CreateMapper();
        }

        [Test]
        public void ToLdapEntity_Value()
        {
            LdapEntry entry = Generators.CreateEntry();

            var entity = entry.ToLdapEntity<LdapUser>(_mapper);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(entry.DN, entity.DistinguishedName);

                Assert.AreEqual(entry.getAttribute("dn").StringValue, entity.DistinguishedName);
                Assert.AreEqual(entry.getAttribute("cn").StringValue, entity.CommonName);
                Assert.AreEqual(entry.getAttribute("gn").StringValue, entity.GivenName);
            });
        }

        [Test]
        public void ToNovellEntry_Value()
        {
            LdapUser entity = Generators.CreateUser();

            LdapEntry entry = entity.ToNovellEntry(_mapper);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(entity?.DistinguishedName, entry.DN);

                Assert.AreEqual(entity?.DistinguishedName, entry.getAttribute("dn").StringValue);
                Assert.AreEqual(entity?.CommonName, entry.getAttribute("cn").StringValue);
                Assert.AreEqual(entity?.GivenName, entry.getAttribute("gn").StringValue);
            });
        }
    }
}
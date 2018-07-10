using LinqToLdap.Mapping;
using NUnit.Framework;
using SimpleLdap.Attributes;
using SimpleLdap.Helpers;
using SimpleLdap.Providers;
using SimpleLdap.Tests.Models;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class GenericMapperUnitTests
    {
        private static LdapUser CreateUser() => new LdapUser
        {
            DistinguishedName = "gosho",
            GivenName = "pesho",
            CommonName = "sasho"
        };

        [Test]
        public void TestMapping_ActiveDirectory_HasCountAndObjectClasses()
        {
            var providerAd = new ActiveDirectoryLdapProvider();
            var mapperAd = new LdapAttributeMapper<ActiveDirectoryLdapProvider>(providerAd);
            var classMapperAd = new GenericMapper<LdapUser, ActiveDirectoryLdapProvider>(mapperAd);

            Assert.Multiple(() =>
            {
//                Assert.That(classMapperAd.PropertyMappings, Has.Count.EqualTo(3));
//                Assert.AreEqual(mapperAd.ObjectClasses[LdapEntityType.User],
//                    classMapperAd.ToObjectMapping().ObjectClasses);
            });
        }


        [Test]
        public void TestMapping_OpenLdap_HasCountAndObjectClasses()
        {
            var providerOpenLdap = new OpenLdapProvider();
            var mapperOpenLdap = new LdapAttributeMapper<OpenLdapProvider>(providerOpenLdap);
            var directoryMapperOpenLdap = new DirectoryMapper();
            var classMapperOpenLdap = new GenericMapper<LdapUser, OpenLdapProvider>(mapperOpenLdap);

//            directoryMapperOpenLdap.Map(classMapperOpenLdap);

            Assert.Multiple(() =>
            {
//                Assert.That(classMapperOpenLdap.PropertyMappings, Has.Count.EqualTo(3));
//                Assert.AreEqual(mapperOpenLdap.ObjectClasses[LdapEntityType.User],
//                    classMapperOpenLdap.ToObjectMapping().ObjectClasses);
            });
        }
    }
}
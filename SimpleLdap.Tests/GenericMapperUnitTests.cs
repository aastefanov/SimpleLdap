using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using LinqToLdap.Mapping;
using NUnit.Framework;
using SimpleLdap.Attributes;
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
        public void TestMapping_ActiveDirectory_HasCountAndObjectClass()
        {
            var mapper = new LdapAttributeMapper<ActiveDirectoryLdapProvider>();
            var directoryMapper = new DirectoryMapper();
            var classMapper = new GenericMapper<LdapUser, ActiveDirectoryLdapProvider>(mapper);

            directoryMapper.Map(classMapper);
            
            Assert.Multiple(() =>
            {
                Assert.That(classMapper.PropertyMappings, Has.Count.EqualTo(3));
                Assert.AreEqual(mapper.ObjectClasses[LdapEntityType.User],
                    classMapper.ToObjectMapping().ObjectClasses.First());
            });
        }
    }
}
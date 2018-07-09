﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using LinqToLdap.Mapping;
using NSubstitute;
using NUnit.Framework;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;
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
            var directoryMapperAd = new DirectoryMapper();
            var classMapperAd = new GenericMapper<LdapUser, ActiveDirectoryLdapProvider>(mapperAd);

            directoryMapperAd.Map(classMapperAd);

            Assert.Multiple(() =>
            {
                Assert.That(classMapperAd.PropertyMappings, Has.Count.EqualTo(3));
                Assert.AreEqual(mapperAd.ObjectClasses[LdapEntityType.User],
                    classMapperAd.ToObjectMapping().ObjectClasses);
            });
        }


        [Test]
        public void TestMapping_OpenLdap_HasCountAndObjectClasses()
        {
            var providerOpenLdap = new OpenLdapProvider();
            var mapperOpenLdap = new LdapAttributeMapper<OpenLdapProvider>(providerOpenLdap);
            var directoryMapperOpenLdap = new DirectoryMapper();
            var classMapperOpenLdap = new GenericMapper<LdapUser, OpenLdapProvider>(mapperOpenLdap);

            directoryMapperOpenLdap.Map(classMapperOpenLdap);

            Assert.Multiple(() =>
            {
                Assert.That(classMapperOpenLdap.PropertyMappings, Has.Count.EqualTo(3));
                Assert.AreEqual(mapperOpenLdap.ObjectClasses[LdapEntityType.User],
                    classMapperOpenLdap.ToObjectMapping().ObjectClasses);
            });
        }
    }
}
using System.Collections.Generic;
using Novell.Directory.Ldap;
using NSubstitute;
using SimpleLdap.Attributes;
using SimpleLdap.Expressions.Visitors;
using SimpleLdap.Interfaces;
using SimpleLdap.Tests.Models;
using LdapAttribute = SimpleLdap.Attributes.LdapAttribute;

namespace SimpleLdap.Tests.Helpers
{
    internal static class Generators
    {
        internal static ILdapProvider CreateProvider()
        {
            var provider = Substitute.For<ILdapProvider>();

            provider.AttributeNames.Returns(new Dictionary<LdapAttribute, string>
            {
                {LdapAttribute.DistinguishedName, "dn"},
                {LdapAttribute.FullName, "cn"},
                {LdapAttribute.FirstName, "gn"}
            });

            provider.ObjectClasses.Returns(new Dictionary<LdapEntityType, IEnumerable<string>>
            {
                {LdapEntityType.User, new List<string> {"inetOrgPerson", "person", "top"}},
                {LdapEntityType.Group, new List<string> {"group", "top"}},
                {LdapEntityType.OrganizationalUnit, new List<string> {"organizationalUnit", "top"}}
            });

            return provider;
        }

        internal static LdapAttributeMapper CreateMapper() => new LdapAttributeMapper(CreateProvider());

        internal static QueryTranslator CreateTranslator() => new QueryTranslator(CreateMapper());

        internal static LdapUser CreateUser()
        {
            return new LdapUser
            {
                DistinguishedName = "cn=fakeuser,dc=example,dc=com",
                CommonName = "Fake User",
                GivenName = "Fake"
            };
        }

        internal static LdapEntry CreateEntry()
        {
            var attributeSet = new LdapAttributeSet
            {
                new Novell.Directory.Ldap.LdapAttribute("dn", "cn=fakeuser,dc=example,dc=com"),
                new Novell.Directory.Ldap.LdapAttribute("cn", "Fake User"),
                new Novell.Directory.Ldap.LdapAttribute("gn", "Fake")
            };


            var entry = new LdapEntry("cn=fakeuser,dc=example,dc=com", attributeSet);

            return entry;
        }
    }
}
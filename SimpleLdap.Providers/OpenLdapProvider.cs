using System;
using System.Collections.Generic;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Providers
{
    public class OpenLdapProvider : ILdapProvider
    {
        public IDictionary<LdapAttribute, string> AttributeNames => new Dictionary<LdapAttribute, string>
        {
            {LdapAttribute.DistinguishedName, "DN"},
            {LdapAttribute.ObjectClass, "objectClass"},
            {LdapAttribute.FirstName, "givenName"},
            {LdapAttribute.FullName, "cn"}
        };

        public IDictionary<LdapEntityType, string> ObjectClasses => new Dictionary<LdapEntityType, string>
        {
            {LdapEntityType.User, "person"},
            {LdapEntityType.Group, "group"},
            {LdapEntityType.OrganizationalUnit, "organizationalUnit"}
        };
    }
}
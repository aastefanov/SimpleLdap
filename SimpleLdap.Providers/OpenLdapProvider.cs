using System;
using System.Collections.Generic;

namespace SimpleLdap.Providers
{
    public class OpenLdapProvider : ILdapProvider
    {
        public IDictionary<LdapAttribute, string> AttributeNames => new Dictionary<LdapAttribute, string>
        {
            {LdapAttribute.DistinguishedName, "DN"},
            {LdapAttribute.ObjectClass, "objectClass"},
            {LdapAttribute.GivenName, "givenName"},
            {LdapAttribute.CommonName, "cn"}
        };
    }
}
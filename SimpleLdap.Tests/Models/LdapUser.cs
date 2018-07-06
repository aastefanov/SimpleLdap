using System;
using LinqToLdap.Mapping;

namespace SimpleLdap.Tests.Models
{
    public class LdapUser : LdapEntity
    {
        [LdapAttribute(LdapAttribute.DistinguishedName)] public new string DistinguishedName { get; set; }

        [LdapAttribute(LdapAttribute.GivenName)] public string GivenName { get; set; }
        
        [LdapAttribute(LdapAttribute.CommonName)] public string CommonName { get; set; }
    }
}
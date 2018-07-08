using System;
using System.Collections.Generic;
using LinqToLdap.Mapping;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Tests.Models
{
    [LdapEntity(LdapEntityType.User)]
    public class LdapUser : ILdapEntity
    {
        [LdapAttribute(LdapAttribute.DistinguishedName, true)]
        public string DistinguishedName { get; set; }

        [LdapAttribute(LdapAttribute.FirstName)]
        public string GivenName { get; set; }

        [LdapAttribute(LdapAttribute.FullName)]
        public string CommonName { get; set; }
    }
}
using System.Collections.Generic;
using LinqToLdap.Mapping;
using SimpleLdap.Attributes;

namespace SimpleLdap.Interfaces
{
    public interface ILdapEntity
    {
        [LdapAttribute(LdapAttribute.DistinguishedName, true)]
        string DistinguishedName { get; set; }
    }
}
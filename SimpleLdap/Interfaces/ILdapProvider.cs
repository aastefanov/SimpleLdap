using System.Collections.Generic;
using SimpleLdap.Attributes;

namespace SimpleLdap.Interfaces
{
    public interface ILdapProvider
    {
        IDictionary<LdapAttribute, string> AttributeNames { get; }
        IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses { get; }
    }
}
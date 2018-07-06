using System.Collections.Generic;

namespace SimpleLdap
{
    public interface ILdapProvider
    {
        IDictionary<LdapAttribute, string> AttributeNames { get; }
    }
}
using System;
using System.Diagnostics.SymbolStore;

namespace SimpleLdap.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LdapAttributeAttribute : Attribute
    {
        public LdapAttribute Attribute { get; }
        public bool IsReadOnly { get; }
        public bool IsDistinguishedName { get; }

        public LdapAttributeAttribute(LdapAttribute attribute, bool isReadOnly = false)
        {
            Attribute = attribute;
            IsDistinguishedName = attribute == LdapAttribute.DistinguishedName;
            IsReadOnly = isReadOnly;
        }
    }
}
using System;

namespace SimpleLdap.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LdapAttributeAttribute : Attribute
    {
        public LdapAttribute Attribute { get; }
        public bool IsDistinguishedName { get; }

        public LdapAttributeAttribute(LdapAttribute attribute)
        {
            Attribute = attribute;
            IsDistinguishedName = attribute == LdapAttribute.DistinguishedName;
        }
    }
}
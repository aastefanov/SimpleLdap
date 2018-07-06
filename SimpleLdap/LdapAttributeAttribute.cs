using System;

namespace SimpleLdap
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LdapAttributeAttribute : Attribute
    {
        public readonly LdapAttribute Attribute;

        public LdapAttributeAttribute(LdapAttribute attribute)
        {
            Attribute = attribute;
        }
    }
}
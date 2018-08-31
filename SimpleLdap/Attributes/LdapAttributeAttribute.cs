using System;

namespace SimpleLdap.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class LdapAttributeAttribute : Attribute
    {
        public LdapAttribute Attribute { get; }

        public LdapAttributeAttribute(LdapAttribute attribute)
        {
            Attribute = attribute;
        }
    }
}
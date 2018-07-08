using System;

namespace SimpleLdap.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class LdapEntityAttribute : Attribute
    {
        public readonly LdapEntityType EntityType;

        public LdapEntityAttribute(LdapEntityType type)
        {
            EntityType = type;
        }
    }
}
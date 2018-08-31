using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Helpers
{
    internal static class Mappings
    {
        internal static Dictionary<LdapAttributeAttribute, PropertyInfo> GetAttributes<T>() =>
            typeof(T).GetProperties()
                .Where(prop => prop.IsDefined(typeof(LdapAttributeAttribute), false))
                .ToDictionary(x => x.GetCustomAttribute<LdapAttributeAttribute>(), y => y);

        internal static LdapEntityType GetEntityType<T>() where T : ILdapEntity
        {
            return typeof(T).GetCustomAttribute<LdapEntityAttribute>().EntityType;
        }
    }
}
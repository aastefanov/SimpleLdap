using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Helpers
{
    internal static class Mappings
    {
        internal static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ILdapEntity entity)
            where TAttribute : Attribute
        {
            var t = entity.GetType();
            var properties = t.GetProperties();
            return properties.Select(x => x.GetCustomAttribute<TAttribute>());
        }


        internal static IEnumerable<LdapAttribute> GetLdapAttributes<TEntity, TProvider>(this TEntity entity,
            LdapAttributeMapper<TProvider> mapper)
            where TEntity : class, ILdapEntity
            where TProvider : ILdapProvider
        {
            var genericMapper = new GenericMapper<TEntity, TProvider>(mapper);
            var attributes = genericMapper.PerformMapping(entity);
            return new List<LdapAttribute> { };
        }


        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttributes<T>() where T : class, ILdapEntity
        {
            return typeof(T).GetProperties()
                .Where(prop => prop.IsDefined(typeof(LdapAttributeAttribute), false));
        }

        internal static LdapEntityType GetEntityType<T>() where T : class, ILdapEntity
        {
            return typeof(T).GetCustomAttribute<LdapEntityAttribute>().EntityType;
        }
    }
}
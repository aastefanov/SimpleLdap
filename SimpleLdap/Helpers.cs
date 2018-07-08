using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleLdap.Interfaces;

namespace SimpleLdap
{
    public static class Helpers
    {
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ILdapEntity entity)
            where TAttribute : Attribute
        {
            var t = entity.GetType();
            var properties = t.GetProperties();
            return properties.Select(x => x.GetCustomAttribute<TAttribute>());
        }
    }
}
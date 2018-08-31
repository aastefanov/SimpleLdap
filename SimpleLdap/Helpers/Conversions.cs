using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Novell.Directory.Ldap;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;
using LdapAttribute = Novell.Directory.Ldap.LdapAttribute;

namespace SimpleLdap.Helpers
{
    public static class Conversions
    {
        /// <summary>
        /// Converts <see cref="ILdapEntity"/> to a Novell <see cref="LdapEntry"/>
        /// </summary>
        /// <param name="entity">The entity to be converted</param>
        /// <param name="mapper">Mapper providing attribute names</param>
        /// <typeparam name="T">A <see cref="ILdapEntity"/> to be converted</typeparam>
        /// <returns></returns>
        public static LdapEntry ToNovellEntry<T>(this T entity, LdapAttributeMapper mapper) where T : ILdapEntity
        {
            var propertiesWithAttributes = Mappings.GetAttributes<T>();

            var attributeSet = new LdapAttributeSet();
            foreach (var property in propertiesWithAttributes)
            {
                string attributeName = mapper.GetAttributeKey(property.Key);
                attributeSet.Add(new LdapAttribute(attributeName,
                    property.Value.GetValue(entity).ToString()));
            }

            return new LdapEntry(entity.DistinguishedName, attributeSet);
        }

        /// <summary>
        /// Converts Novell <see cref="LdapEntry"/> to a <see cref="ILdapEntity"/>
        /// </summary>
        /// <param name="entry">Novell entry</param>
        /// <param name="mapper">Mapper providing attribute names</param>
        /// <typeparam name="T">The resulting entity type</typeparam>
        /// <returns></returns>
        public static T ToLdapEntity<T>(this LdapEntry entry, LdapAttributeMapper mapper) where T : ILdapEntity
        {
            Dictionary<LdapAttributeAttribute, PropertyInfo> propertiesWithAttributes = Mappings.GetAttributes<T>();
            
            LdapAttributeSet attributeSet = entry.getAttributeSet();

            var entity = (T) FormatterServices.GetUninitializedObject(typeof(T));

            foreach (var property in propertiesWithAttributes)
            {
                string attributeName = mapper.GetAttributeKey(property.Key);
                property.Value.SetValue(entity, attributeSet.getAttribute(attributeName).StringValue);
            }

            if (entity.DistinguishedName == null) entity.DistinguishedName = entry.DN;
            return entity;
        }

        public static string ToLdapOperator(this ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.AndAlso:
                    return "&";
                case ExpressionType.OrElse:
                    return "|";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.Not:
                    return "!";
                default:
                    throw new ArgumentException($"Unsupported operator {expressionType}", nameof(expressionType));
            }
        }
    }
}
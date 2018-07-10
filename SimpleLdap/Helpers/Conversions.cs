using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Novell.Directory.Ldap;
using SimpleLdap.Interfaces;
using LdapAttribute = Novell.Directory.Ldap.LdapAttribute;

namespace SimpleLdap.Helpers
{
    public static class Conversions
    {
        public static LdapEntry ToLdapEntry<TProvider>(this ILdapEntity entity, LdapAttributeMapper<TProvider> mapper)
            where TProvider : ILdapProvider
        {
            var attributeSet = new LdapAttributeSet();
            attributeSet.AddAll(entity.GetLdapAttributes(mapper).ToNonGenericCollection());
            return new LdapEntry(entity.DistinguishedName, attributeSet);
        }

        private static ICollection ToNonGenericCollection<T>(this IEnumerable<T> collection)
        {
            var newCollection = new ArrayList();
            foreach (var element in collection)
                newCollection.Add(element);
            return newCollection;
        }


        public static T ToLdapEntity<T>(this LdapEntry entry) where T : class, ILdapEntity, new()
        {
            var attributeSet = entry.getAttributeSet();
            var entity = new T();

            return entity;
        }

        public static string ToLdapOperator(this ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.AndAlso:
                    return "&";
                case ExpressionType.OrElse:
                    return "|";
                case ExpressionType.Not:
                    return "!";
                
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "!";
                
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                default:
                    throw new ArgumentException($"Unspported operator {nodeType}");
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Novell.Directory.Ldap;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;
using LdapAttribute = Novell.Directory.Ldap.LdapAttribute;

namespace SimpleLdap.Helpers
{
    public class GenericMapper<T, TProvider>
        where T : class, ILdapEntity where TProvider : ILdapProvider
    {
        private readonly LdapAttributeMapper<TProvider> _mapper;

        public GenericMapper(LdapAttributeMapper<TProvider> mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<LdapAttribute> PerformMapping(T entity)
        {
            var set = new List<LdapAttribute>();

            var propertiesWithAttributes = Mappings.GetPropertiesWithAttributes<T>()
                .ToDictionary(x => x, y => y.GetCustomAttribute<LdapAttributeAttribute>());

            foreach (var property in propertiesWithAttributes)
            {
                var value = property.Key.GetValue(entity) as string[] ?? new string[]
                {
                    property.Key.GetValue(entity) as string
                };

                var attribute = new LdapAttribute(_mapper.GetAttributeKey(property.Value.Attribute),
                    value);

                set.Add(attribute);
            }

            return set;
        }
    }
}
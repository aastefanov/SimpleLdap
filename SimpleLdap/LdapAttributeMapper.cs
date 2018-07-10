using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinqToLdap.Mapping;
using SimpleLdap;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap
{
    public class LdapAttributeMapper<TProvider>
        where TProvider : ILdapProvider
    {
        private readonly TProvider _provider;

        public IDictionary<LdapAttribute, string> Mappings => _provider.AttributeNames;
        public IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses => _provider.ObjectClasses;

        public LdapAttributeMapper(TProvider provider)
        {
            _provider = provider;
        }

        public string GetAttributeKey(LdapAttribute attribute)
        {
            if (Mappings.ContainsKey(attribute)) return Mappings[attribute];
            throw new ArgumentException();
        }

        public IEnumerable<string> GetObjectClasses(LdapEntityType entityType)
        {
            if (ObjectClasses.ContainsKey(entityType)) return ObjectClasses[entityType];
            throw new ArgumentException();
        }
    }
}


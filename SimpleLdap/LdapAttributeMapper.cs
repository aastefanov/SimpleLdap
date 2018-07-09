using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using LinqToLdap.Mapping;
using SimpleLdap;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap
{
    public class LdapAttributeMapper<TProvider>
        where TProvider : ILdapProvider
    {
        private readonly ILdapProvider _provider;

        public IDictionary<LdapAttribute, string> Mappings => _provider.AttributeNames;
        public IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses => _provider.ObjectClasses;

        public LdapAttributeMapper(ILdapProvider provider)
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

public class GenericMapper<T, TProvider> : ClassMap<T>
    where T : class, ILdapEntity where TProvider : ILdapProvider, new()
{
    private readonly LdapAttributeMapper<TProvider> _mapper;

    public GenericMapper(LdapAttributeMapper<TProvider> mapper) : base()
    {
        _mapper = mapper;
    }

    /// TODO: Active directory ObjectCategory mapping
    public override IClassMap PerformMapping(string namingContext = null, string objectCategory = null,
        bool includeObjectCategory = true,
        IEnumerable<string> objectClasses = null, bool includeObjectClasses = true)
    {
        NamingContext(namingContext);

        // ObjectCategory("top");
        ObjectClasses(_mapper.GetObjectClasses(GetEntityType()) ?? objectClasses);

        Dictionary<PropertyInfo, LdapAttributeAttribute> propertiesWithAttributes = GetPropertiesWithAttributes()
            .ToDictionary(x => x, y =>
                y.GetCustomAttribute<LdapAttributeAttribute>());

        foreach (var property in propertiesWithAttributes)
        {
            var map = MapPropertyInfo(property.Key, false, property.Value.IsDistinguishedName)
                .Named(_mapper.GetAttributeKey(property.Value.Attribute));
            if (property.Value.IsReadOnly) map.ReadOnly();
        }

        return this;
    }

    private static IEnumerable<PropertyInfo> GetPropertiesWithAttributes()
    {
        return typeof(T).GetProperties()
            .Where(prop => prop.IsDefined(typeof(LdapAttributeAttribute), false));
    }

    private static LdapEntityType GetEntityType()
    {
        return typeof(T).GetCustomAttribute<LdapEntityAttribute>().EntityType;
    }
}
using System.Collections.Generic;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap
{
    /// <summary>
    /// Makes the mapping between <see cref="LdapAttribute"/> and server-specific attribute names
    /// based on <see cref="ILdapProvider"/>
    /// </summary>
    public class LdapAttributeMapper
    {
        private readonly ILdapProvider _provider;

        private IDictionary<LdapAttribute, string> Mappings => _provider.AttributeNames;

        private IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses => _provider.ObjectClasses;

        public LdapAttributeMapper(ILdapProvider provider)
        {
            _provider = provider;
        }

        public string GetAttributeKey(LdapAttribute attribute)
        {
            return Mappings.ContainsKey(attribute) ? Mappings[attribute] : null;
        }

        public string GetAttributeKey(LdapAttributeAttribute attribute) => GetAttributeKey(attribute.Attribute);

        public IEnumerable<string> GetObjectClasses(LdapEntityType entityType)
        {
            return ObjectClasses.ContainsKey(entityType) ? ObjectClasses[entityType] : null;
        }
    }
}
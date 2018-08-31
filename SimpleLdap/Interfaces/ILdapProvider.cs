using System.Collections.Generic;
using SimpleLdap.Attributes;

namespace SimpleLdap.Interfaces
{
    /// <summary>
    /// Provides mappings from attribute name to a server-specific string
    /// </summary>
    public interface ILdapProvider
    {
        /// <summary>
        /// Maps <see cref="LdapAttribute"/> to a string
        /// </summary>
        IDictionary<LdapAttribute, string> AttributeNames { get; }
        /// <summary>
        /// Maps <see cref="LdapEntityType"/> to object classes
        /// </summary>
        IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses { get; }
    }
}
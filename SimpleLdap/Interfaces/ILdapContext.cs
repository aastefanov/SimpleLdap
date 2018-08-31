using System.Collections.Generic;
using System.Linq;
using Novell.Directory.Ldap;

namespace SimpleLdap.Interfaces
{
    /// <summary>
    /// Provides methods for executing commands on LDAP server
    /// </summary>
    public interface ILdapContext
    {
        void Add<T>(T entity) where T : ILdapEntity;

        void Update<T>(T entity, IEnumerable<LdapModification> modifications) where T : ILdapEntity;

        void Delete<T>(T entity) where T : ILdapEntity;
        void Delete(string distinguishedName);

        IQueryable<T> Query<T>();
    }
}
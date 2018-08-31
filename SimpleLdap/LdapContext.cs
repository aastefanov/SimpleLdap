using System;
using System.Collections.Generic;
using System.Linq;
using Novell.Directory.Ldap;
using SimpleLdap.Helpers;
using SimpleLdap.Interfaces;

namespace SimpleLdap
{
    public class LdapContext : ILdapContext
    {
        private readonly ILdapConfiguration _configuration;

        private ILdapConnection Connect()
        {
            // Creating an LdapConnection instance 
            var ldapConn = new LdapConnection {SecureSocketLayer = _configuration.UseSsl};

            ldapConn.Connect(_configuration.ServerName,
                _configuration.UseSsl ? _configuration.Port : _configuration.SslPort);

            ldapConn.Bind(_configuration.BindUser, _configuration.BindPassword);

            return ldapConn;
        }

        private readonly LdapAttributeMapper _mapper;

        public LdapContext(ILdapConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = new LdapAttributeMapper(_configuration.Provider);
        }


        public void Add<T>(T entity) where T : ILdapEntity
        {
            using (ILdapConnection connection = Connect())
            {
                var entry = entity.ToNovellEntry(_mapper);
                connection.Add(entry);
            }
        }

        public void Update<T>(T entity, IEnumerable<LdapModification> modifications) where T : ILdapEntity
        {
            using (ILdapConnection connection = Connect())
            {
                connection.Modify(entity.DistinguishedName, modifications.ToArray());
            }
        }

        public void Delete<T>(T entity) where T : ILdapEntity
        {
            using (ILdapConnection connection = Connect())
            {
                connection.Delete(entity.DistinguishedName);
            }
        }

        public void Delete(string distinguishedName)
        {
            using (ILdapConnection connection = Connect())
            {
                connection.Delete(distinguishedName);
            }
        }

        public IQueryable<T> Query<T>()
        {
            throw new NotImplementedException();
        }
    }
}
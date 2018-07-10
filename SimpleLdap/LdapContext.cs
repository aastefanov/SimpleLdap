using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqToLdap;
using System.Linq;
using Novell.Directory.Ldap;
using SimpleLdap.Helpers;
using SimpleLdap.Interfaces;

namespace SimpleLdap
{
    public class LdapContext<TProvider> : ILdapContext where TProvider : ILdapProvider
    {
        private static ILdapConnection Connect()
        {
            // Creating an LdapConnection instance 
            var ldapConn = new LdapConnection() {SecureSocketLayer = true};

            //Connect function will create a socket connection to the server - Port 389 for insecure and 3269 for secure    
            ldapConn.Connect("ADServrName", 3269);

            //Bind function with null user dn and password value will perform anonymous bind to LDAP server 
            ldapConn.Bind(@"domain\username", "password");

            return ldapConn;
        }

        private readonly TProvider _provider;
        private readonly LdapAttributeMapper<TProvider> _mapper;

        public LdapContext(TProvider provider)
        {
//            _configuration = configuration;
            _provider = provider;
            _mapper = new LdapAttributeMapper<TProvider>(_provider);
        }

//        private IDirectoryContext Connect()
//        {
//            return new DirectoryContext(_configuration);
//        }

        public void Add<T>(T entity) where T : class, ILdapEntity
        {
            using (var connection = Connect())
            {
                var entry = entity.ToLdapEntry(_mapper);
                connection.Add(entry);
            }
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> expr) where T : class, ILdapEntity
        {
//            using (var context = Connect())
//            {
//                return context.Query<T>().Where(expr);
//            }

            using (var connection = Connect())
            {
//                var filterBuilder = new LdapFilterBuilder();
                var filterConditions = new List<string>
                {
                    "objectClass=person",
                    "objectCategory=top"
                };
                return connection.Search("", 1,
//                    filterBuilder.GetFilterCondition(filterConditions, LdapFilterBuilder.LdapFilterExpression.AND),
                    "",
                    new string[0],
                    false).Cast<T>();
            }
        }

        public T FindByDistinguishedName<T>(string distinguishedName) where T : class, ILdapEntity, new()
        {
            using (var connection = Connect())
            {
                return connection.Read(distinguishedName).ToLdapEntity<T>();
            }
        }

        public void Update<T>(T entity) where T : class, ILdapEntity
        {
            using (var connection = Connect())
            {
                connection.Modify(entity.DistinguishedName, new LdapModification[] { });
            }
        }

        public void Delete<T>(T entity) where T : class, ILdapEntity
        {
            using (var connection = Connect())
            {
                connection.Delete(entity.DistinguishedName);
            }
        }

        public void Delete(string distinguishedName)
        {
            using (var connection = Connect())
            {
                connection.Delete(distinguishedName);
            }
        }
    }
}
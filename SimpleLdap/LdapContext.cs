using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqToLdap;
using System.Linq;
using System.Reflection;
using SimpleLdap.Interfaces;

namespace SimpleLdap
{

    public class LdapContext<TProvider> where TProvider : ILdapProvider, new()
    {
        private readonly LdapConfiguration _configuration;
        private readonly ILdapProvider _provider = new TProvider();
        private readonly LdapAttributeMapper<TProvider> _mapper = new LdapAttributeMapper<TProvider>();
        
        public LdapContext(LdapConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDirectoryContext Connect() => new DirectoryContext(_configuration);

        public void Add<T>(T entity) where T : class, ILdapEntity
        {
            using (var context = Connect())
            {
                context.Add(entity);
            }
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> expr) where T : class, ILdapEntity
        {
            using (var context = Connect())
            {
                return context.Query<T>().Where(expr);
            }
        }

        public T AddAndGet<T>(T entity) where T : class, ILdapEntity
        {
            using (var context = Connect())
            {
                return context.AddAndGet(entity);
            }
        }

        public T UpdateAndGet<T>(T entity) where T : class, ILdapEntity
        {
            using (var context = Connect())
            {
                return context.UpdateAndGet(entity);
            }
        }

        public void Remove<T>(T entity) where T : class, ILdapEntity
        {
            using (var context = Connect())
            {
//                return context.
            }
        }
    }
}
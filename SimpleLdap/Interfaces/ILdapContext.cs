using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleLdap.Interfaces
{
    public interface ILdapContext
    {
        IEnumerable<T> Where<T>(Expression<Func<T, bool>> expr) where T : class, ILdapEntity;
        T FindByDistinguishedName<T>(string distinguishedName) where T : class, ILdapEntity, new();
        
        void Add<T>(T entity) where T : class, ILdapEntity;
        
        void Update<T>(T entity) where T : class, ILdapEntity;
        
        void Delete<T>(T entity) where T : class, ILdapEntity;
        void Delete(string distinguishedName);
    }
}
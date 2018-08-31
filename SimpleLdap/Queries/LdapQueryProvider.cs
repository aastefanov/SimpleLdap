using System;
using System.Linq;
using System.Linq.Expressions;
using SimpleLdap.Expressions.Visitors;

namespace SimpleLdap.Queries
{
    public class LdapQueryProvider : IQueryProvider, IDisposable
    {
        private readonly LdapAttributeMapper _mapper;
        public LdapQueryProvider(LdapAttributeMapper mapper)
        {
            _mapper = mapper;
        }
        
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new LdapQuery<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
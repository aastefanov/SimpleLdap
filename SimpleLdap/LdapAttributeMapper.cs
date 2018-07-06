using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleLdap
{
    public class LdapAttributeMapper<TProvider>
        where TProvider : ILdapProvider, new()
    {
        private readonly ILdapProvider _provider;

        public IDictionary<LdapAttribute, string> Mappings => _provider.AttributeNames;

        public LdapAttributeMapper()
        {
            _provider = new TProvider();
        }

        public string Map(LdapAttribute attribute)
        {
            if (Mappings.ContainsKey(attribute)) return Mappings[attribute];
            throw new ArgumentException();
        }
    }
}
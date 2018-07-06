using System;

namespace SimpleLdap
{
    public interface ILdapServerConfiguration<TAttribute>
    {
        ILdapProvider Provider { get; set; }
    }
}
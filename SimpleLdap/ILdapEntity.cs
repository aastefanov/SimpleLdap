using LinqToLdap.Mapping;

namespace SimpleLdap
{
    public interface ILdapEntity
    {
        [DistinguishedName]
        string DistinguishedName { get; set; }
    }

    public abstract class LdapEntity : ILdapEntity
    {
        [DistinguishedName]
        public string DistinguishedName { get; set; }
    }
}
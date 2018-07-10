using SimpleLdap.Attributes;

namespace SimpleLdap.Interfaces
{
    public interface ILdapEntity
    {
        [LdapAttribute(LdapAttribute.DistinguishedName)]
        string DistinguishedName { get; set; }
    }

    public abstract class LdapEntityBase : ILdapEntity
    {
        [LdapAttribute(LdapAttribute.DistinguishedName)]
        public string DistinguishedName { get; set; }
    }
}
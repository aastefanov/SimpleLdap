using SimpleLdap.Attributes;

namespace SimpleLdap.Interfaces
{
    public interface ILdapEntity
    {
        [LdapAttribute(LdapAttribute.DistinguishedName)]
        string DistinguishedName { get; set; }
    }
}
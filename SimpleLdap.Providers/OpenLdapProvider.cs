using System.Collections.Generic;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Providers
{
    public class OpenLdapProvider : ILdapProvider
    {
        public IDictionary<LdapAttribute, string> AttributeNames => new Dictionary<LdapAttribute, string>
        {
            {LdapAttribute.DistinguishedName, "dn"},
            {LdapAttribute.FirstName, "gn"},
            {LdapAttribute.FullName, "cn"},
            {LdapAttribute.HomeNumber, "homePhone"},
            {LdapAttribute.Country, "co"},
            {LdapAttribute.UserPhoto, "jpegPhoto"},
            {LdapAttribute.MobileNumber, "mobile"},
            {LdapAttribute.EmailAddress, "mail"},
            {LdapAttribute.Pager, "pager"},
            {LdapAttribute.LastName, "surname"},
            {LdapAttribute.StreetAddress, "street"},
            {LdapAttribute.Password, "userPassword"},
            {LdapAttribute.LogonName, "uid"},
            {LdapAttribute.State, "stateOrProvinceName"},
            {LdapAttribute.ZipCode, "postalCode"},
            {LdapAttribute.TelephoneNumber, "facsimileTelephoneNumber"}
        };

        public IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses =>
            new Dictionary<LdapEntityType, IEnumerable<string>>
            {
                {
                    LdapEntityType.User, new List<string> {"account", "top"}
                },
                {
                    LdapEntityType.Group, new List<string> {"groupOfUniqueNames", "top"}
                },
                {
                    LdapEntityType.OrganizationalUnit, new List<string> {"organizationalUnit", "top"}
                }
            };
    }
}
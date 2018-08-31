using System.Collections.Generic;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;
using static SimpleLdap.Attributes.LdapAttribute;
using static SimpleLdap.Attributes.LdapEntityType;

namespace SimpleLdap.Providers
{
    public class OpenLdapProvider : ILdapProvider
    {
        public IDictionary<LdapAttribute, string> AttributeNames => new Dictionary<LdapAttribute, string>
        {
            {DistinguishedName, "dn"},
            {FirstName, "gn"},
            {FullName, "cn"},
            {HomeNumber, "homePhone"},
            {Country, "co"},
            {UserPhoto, "jpegPhoto"},
            {MobileNumber, "mobile"},
            {EmailAddress, "mail"},
            {Pager, "pager"},
            {LastName, "surname"},
            {StreetAddress, "street"},
            {Password, "userPassword"},
            {LogonName, "uid"},
            {State, "stateOrProvinceName"},
            {ZipCode, "postalCode"},
            {TelephoneNumber, "facsimileTelephoneNumber"}
        };

        public IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses =>
            new Dictionary<LdapEntityType, IEnumerable<string>>
            {
                {
                    User, new List<string> {"account", "top"}
                },
                {
                    Group, new List<string> {"groupOfUniqueNames", "top"}
                },
                {
                    OrganizationalUnit, new List<string> {"organizationalUnit", "top"}
                }
            };
    }
}
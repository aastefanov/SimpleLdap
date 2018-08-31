using System.Collections.Generic;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;
using static SimpleLdap.Attributes.LdapAttribute;
using static SimpleLdap.Attributes.LdapEntityType;

namespace SimpleLdap.Providers
{
    public class ActiveDirectoryLdapProvider : ILdapProvider
    {
        public IDictionary<LdapAttribute, string> AttributeNames => new Dictionary<LdapAttribute, string>
            {
                {DistinguishedName, "distinguishedName"},
                {ObjectClass, "objectClass"},

                {FirstName, "givenName"},
                {Initials, "initials"},
                {LastName, "sn"},
                {LogonName, "userPrincipalName"},
                {FullName, "cn"},

                {Description, "description"},

                {Office, "physicalDeliveryOfficeName"},
                {TelephoneNumber, "telephoneNumber"},
                {EmailAddress, "mail"},
                {WebPage, "wWWHomePage"},

                {StreetAddress, "streetAddress"},
                {PostOfficeBox, "postOfficeBox"},
                {City, "l"},
                {State, "st"},
                {ZipCode, "postalCode"},
                {Country, "co"},

                {Password, "password"},
                {AccountExpires, "accountExpires"},
                {UserAccountControl, "userAccountControl"},
                {UserPhoto, "exchangePhoto"},
                {ProfilePath, "profilePath"},
                {LoginScript, "scriptPath"},
                {HomeFolder, "homeDirectory"},
                {HomeDrive, "homeDrive"},
                {LogonTo, "userWorkstations"},

                {HomeNumber, "homePhone"},
                {Pager, "pager"},
                {MobileNumber, "mobile"},
                {Fax, "facsimileTelephoneNumber"},
                {IpPhone, "ipPhone"},

                {Notes, "info"},
                {Title, "title"},
                {Department, "department"},
                {Company, "company"},
                {Manager, "manager"},
                {MailAlias, "mailNickName"},

                {SimpleDisplayName, "displayNamePrintable"},

                {PreventDeletion, "preventDeletion"},
                {ManagerCanUpdateMembers, "managerCanUpdateMembers"},
                {PrimaryGroupId, "primaryGroupID"},
                {ManagedBy, "managedBy"},
                {TargetAddress, "targetAddress"},
                {ProxyAdresses, "proxyAddresses"}
            }
            .Concat(AttributeGroups.EmailAttributes)
            .Concat(AttributeGroups.TerminalServicesAttributes)
            .Concat(AttributeGroups.Office365Attributes);

        public IDictionary<LdapEntityType, IEnumerable<string>> ObjectClasses =>
            new Dictionary<LdapEntityType, IEnumerable<string>>
            {
                {
                    User, new List<string> {"person", "top"}
                },
                {
                    Group, new List<string> {"group", "top"}
                },
                {
                    OrganizationalUnit, new List<string> {"organizationalUnit", "top"}
                }
            };
    }
}
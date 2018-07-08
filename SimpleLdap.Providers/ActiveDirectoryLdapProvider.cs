using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Providers
{
    public class ActiveDirectoryLdapProvider : ILdapProvider
    {
        public IDictionary<LdapAttribute, string> AttributeNames => new Dictionary<LdapAttribute, string>
            {
                {LdapAttribute.DistinguishedName, "DN"},
                {LdapAttribute.ObjectClass, "objectClass"},

                {LdapAttribute.FirstName, "givenName"},
                {LdapAttribute.Initials, "initials"},
                {LdapAttribute.LastName, "sn"},
                {LdapAttribute.LogonName, "userPrincipalName"},
                {LdapAttribute.FullName, "cn"},

                {LdapAttribute.Description, "description"},

                {LdapAttribute.Office, "physicalDeliveryOfficeName"},
                {LdapAttribute.TelephoneNumber, "telephoneNumber"},
                {LdapAttribute.EmailAddress, "mail"},
                {LdapAttribute.WebPage, "wWWHomePage"},

                {LdapAttribute.StreetAddress, "streetAddress"},
                {LdapAttribute.PostOfficeBox, "postOfficeBox"},
                {LdapAttribute.City, "l"},
                {LdapAttribute.State, "st"},
                {LdapAttribute.ZipCode, "postalCode"},
                {LdapAttribute.Country, "co"},

                {LdapAttribute.Password, "password"},
                {LdapAttribute.AccountExpires, "accountExpires"},
                {LdapAttribute.UserAccountControl, "userAccountControl"},
                {LdapAttribute.UserPhoto, "exchangePhoto"},
                {LdapAttribute.ProfilePath, "profilePath"},
                {LdapAttribute.LoginScript, "scriptPath"},
                {LdapAttribute.HomeFolder, "homeDirectory"},
                {LdapAttribute.HomeDrive, "homeDrive"},
                {LdapAttribute.LogonTo, "userWorkstations"},

                {LdapAttribute.HomeNumber, "homePhone"},
                {LdapAttribute.Pager, "pager"},
                {LdapAttribute.MobileNumber, "mobile"},
                {LdapAttribute.Fax, "facsimileTelephoneNumber"},
                {LdapAttribute.IpPhone, "ipPhone"},

                {LdapAttribute.Notes, "info"},
                {LdapAttribute.Title, "title"},
                {LdapAttribute.Department, "department"},
                {LdapAttribute.Company, "company"},
                {LdapAttribute.Manager, "manager"},
                {LdapAttribute.MailAlias, "mailNickName"},

                {LdapAttribute.SimpleDisplayName, "displayNamePrintable"},
                {LdapAttribute.HideFromAddressLists, "msExchHideFromAddressLists"},
            }
            .Concat(AttributeGroups.EmailAttributes)
            .Concat(AttributeGroups.TerminalServicesAttributes)
            .Concat(AttributeGroups.Office365Attributes);

        public IDictionary<LdapEntityType, string> ObjectClasses => new Dictionary<LdapEntityType, string>
        {
            {LdapEntityType.User, "person"},
            {LdapEntityType.Group, "group"},
            {LdapEntityType.OrganizationalUnit, "organizationalUnit"}
        };
    }
}
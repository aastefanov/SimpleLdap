using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Tests.Models
{
    [LdapEntity(LdapEntityType.Group)]
    public class LdapGroup : ILdapEntity
    {
        [LdapAttribute(LdapAttribute.DistinguishedName)]
        public string DistinguishedName { get; set; }
        
        public List<ILdapEntity> Members { get; set; }

        [LdapAttribute(LdapAttribute.TelephoneNumber), DataType(DataType.PhoneNumber)]
        public int? PhoneNumber { get; set; }
    }
}
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using SimpleLdap.Attributes;
using SimpleLdap.Interfaces;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class LdapMapperUnitTests
    {
        [Test]
        public void AttributeMapper_GivenName_GetValue()
        {
            
            var provider = Substitute.For<ILdapProvider>();
            provider.AttributeNames.Returns(new Dictionary<LdapAttribute, string>{{LdapAttribute.FirstName, "gn"}});

            var mapper = new LdapAttributeMapper<ILdapProvider>(provider);
            
            Assert.Multiple(() =>
                {
                    Assert.AreEqual("gn", mapper.GetAttributeKey(LdapAttribute.FirstName));
                }
            );
        }
    }
}
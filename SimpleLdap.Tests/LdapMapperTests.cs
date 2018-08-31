using System.Collections.Generic;
using NUnit.Framework;
using SimpleLdap.Attributes;
using SimpleLdap.Tests.Helpers;

namespace SimpleLdap.Tests
{
    [TestFixture]
    public class LdapMapperTests
    {
        private LdapAttributeMapper _mapper;

        [SetUp]
        public void Setup() => _mapper = Generators.CreateMapper();

        [Test]
        public void AttributeMapper_GetAttribute_HasValue()
        {
            Assert.AreEqual("gn", _mapper.GetAttributeKey(LdapAttribute.FirstName));
        }

        [Test]
        public void AttributeMapper_ObjectClasses_HasValue()
        {
            Assert.That(_mapper.GetObjectClasses(LdapEntityType.User),
                Is.EquivalentTo(new List<string> {"inetOrgPerson", "person", "top"}));
        }
    }
}
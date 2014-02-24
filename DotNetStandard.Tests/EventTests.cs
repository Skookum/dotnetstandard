using DotNetStandard.Tests.Models;
using NUnit.Framework;

namespace DotNetStandard.Tests
{
    public class EventTests
    {
        private EventTest _vent1;
        private EventTest _vent2;

        [SetUp]
        public void Initialize()
        {
            _vent1 = new EventTest("testevent1");
            _vent2 = new EventTest("testevent2");
        }

        [Test]
        public void TestEventTestsAreEqual()
        {
            EventTest ventOne = new EventTest("testevent1");
            Assert.AreEqual(ventOne, _vent1);
        }

        [Test]
        public void TestEventTestsEqualityOperator()
        {
            EventTest ventOne = new EventTest("testevent1");
            Assert.True(ventOne == _vent1);
        }

        [Test]
        public void TestEventTestsAreNotEqual()
        {
            Assert.AreNotEqual(_vent2, _vent1);
        }

        [Test]
        public void TestEventTestsNotEqualOperator()
        {
            Assert.True(_vent2 != _vent1);
        }

        [Test]
        public void TestEventTestReferenceEqualsTrue()
        {
            Assert.AreEqual(_vent1, _vent1);
        }

        [Test]
        public void TestEventTestReferenceEqualsTrueWithEqualsMethod()
        {
            Assert.True(_vent1.Equals(_vent1));
        }

        [Test]
        public void TestEventTestNullReferenceReturnsFalse()
        {
            Assert.False(_vent1.Equals(null));
        }

        [Test]
        public void TestEventTestDoesNotEqualADifferentObject()
        {
            Assert.AreNotEqual("", _vent1);
        }

        [Test]
        public void TestEventTestReferenceEqualsTrueWithEqualsMethodCastObject()
        {
            Assert.True(_vent1.Equals((object)_vent1));
        }

        [Test]
        public void TestEventTestNullReferenceCastObjectReturnsFalse()
        {
            Assert.False(_vent1.Equals((object)null));
        }

        [Test]
        public void TestEventTestDoesNotEqualADifferentObjectCastObject()
        {
            Assert.False(_vent1.Equals((object)""));
        }

        [Test]
        public void TestEventTestHashCodeEquals()
        {
            Assert.AreEqual(_vent1.GetHashCode(), _vent1.GetHashCode());
        }

        [Test]
        public void TestEventTestHashCodeNotEquals()
        {
            Assert.AreNotEqual(_vent1.GetHashCode(), _vent2.GetHashCode());
        }
    }
}
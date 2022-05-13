using System;
using System.Linq;

namespace Presents.Tests
{
using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;

        [SetUp]

        public void SetUp()
        {
            bag = new Bag();
        }

        [Test]
        public void ConstructorInitializer()
        {
            Assert.That(bag,Is.Not.Null);
        }

        [Test]
        public void CtorPresent()
        {
            Present present = new Present("name", 10);
            Assert.AreEqual(present.Name, "name");
            Assert.AreEqual(present.Magic, 10);
        }

        [Test]
        public void CreateBagReturnsNullException()
        {
            Present present = null;
            Assert.Throws<ArgumentNullException>(() => bag.Create(present));
        }

        [Test]
        public void CreateReturnsOperationException()
        {
            Present present = new Present("name", 10);
            bag.Create(present);
            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }

        [Test]
        public void CreateReturnSuccessful()
        {
            Assert.DoesNotThrow(() => bag.Create(new Present("name", 10)));
        }

        [Test]
        public void CreateIsSuccessful()
        {
            Present present = new Present("name", 10);
            bag.Create(present);
            Assert.That(present, Is.EqualTo(bag.GetPresent(present.Name)));
        }

        [Test]
        public void RemoveIsSuccessful()
        {
            Present present = new Present("name", 10);
            bag.Create(present);
            bag.Remove(present);
            Assert.That(bag.GetPresent("name"), Is.Null);

        }

        [Test]
        public void GetPresentWithLeastMagicIsCorrect()
        {
            List<Present> presents = new List<Present>();
            Present present = new Present("name", 10);
            Present present2 = new Present("name2", 20);
            bag.Create(present);
            bag.Create(present2);
            Assert.AreEqual(present, bag.GetPresentWithLeastMagic());

        }

        [Test]
        public void ReturnMessage()
        {
            Present present = new Present("name", 10);
            string expected= $"Successfully added present {present.Name}.";

            string actual = bag.Create(present);

            Assert.AreEqual(expected,actual);

        }

        [Test]
        public void GetPresentIsCorrect()
        {
            Present present = new Present("name", 10);
            Present present2 = new Present("name2", 20);
            bag.Create(present);
            bag.Create(present2);
            Assert.AreEqual(present, bag.GetPresent("name"));

        }

        [Test]
        public void GetPresentsCollection()
        {
            Assert.That(bag.GetPresents(), Is.InstanceOf<IReadOnlyCollection<Present>>());

        }

    }
}

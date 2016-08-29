using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Assets.Scripts.src;

namespace Assets.Scripts.test
{

    [TestFixture]
    class ASimpleClassTest
    {
        private ASimpleClass aSimpleClass;

        [SetUp]
        protected void SetUp()
        {
            aSimpleClass = new ASimpleClass();
        }

        [Test]
        public void AddsTwo()
        {
            Assert.AreEqual(4, aSimpleClass.addTwo(2));
        }
    }
}

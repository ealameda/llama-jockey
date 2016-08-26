using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.test
{
    using System;
    using NUnit.Framework;
    using Assets.Scripts.src;

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

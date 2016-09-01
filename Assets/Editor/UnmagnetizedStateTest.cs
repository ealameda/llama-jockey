using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit.Tests1
{
    [TestFixture]
    class UnmagnetizedStateTest
    {
        private UnmagnetizedState unmagnetizedState;

        [Test]
        public void shouldGoToMagnatizedfromUnmagnatized()
        {
            float magnetStartDistance = 5.0f;
            var statePatternDyamicObject = NSubstitute.Substitute.For<StatePatternDynamicObject>();
            // make new Right pinch detector named "PinchDetector_R" and set it's posistion
            //unmagnatizedState = new UnmagnetizedState();
            unmagnetizedState = new UnmagnetizedState(statePatternDyamicObject, magnetStartDistance);
            unmagnetizedState.ToMagnetizedState();
            Assert.IsInstanceOf(typeof(MagnetizedState), statePatternDyamicObject.currentState);
        }
    }
}

using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace NUnit.Tests1
{
    [TestFixture]
    class StatePatternDynamicObjectTest
    {
        private UnmagnetizedState unmagnetizedState;
        private MagnetizedState magnetizedState;
        private StatePatternDynamicObject statePatternDyamicObject;

        [SetUp]
        void SetUp()
        {
            statePatternDyamicObject = new StatePatternDynamicObject();
            statePatternDyamicObject.Awake();
        }

        [Test]
        public void ShouldGoToMagnatizedfromUnmagnetized()
        {
            float magnetStartDistance = 5.0f;
            unmagnetizedState = new UnmagnetizedState(statePatternDyamicObject, magnetStartDistance);

            unmagnetizedState.ToMagnetizedState();
            Assert.IsInstanceOf(typeof(MagnetizedState), statePatternDyamicObject.currentState);
        }

        [Test]
        public void ShouldNotGoToUnmagnetizedFromUnmagnetized() {
            float magnetStartDistance = 5.0f;
            unmagnetizedState = new UnmagnetizedState(statePatternDyamicObject, magnetStartDistance);

            unmagnetizedState.ToUnmagnetizedState();
            Assert.IsInstanceOf(typeof(MagnetizedState), statePatternDyamicObject.currentState);
        }

        //this will go in a different test class testing unmagnetized logic at unit level
        // make new Right pinch detector named "PinchDetector_R" and set it's posistion
        //unmagnatizedState = new UnmagnetizedState();
    }
}

using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace NUnit.Tests
{
    [TestFixture]
    class StatePatternDynamicObjectTest
    {
        private UnmagnetizedState unmagnetizedState;
        private MagnetizedState magnetizedState;
        private DynamicObjectStateManager statePatternDyamicObject;
        private DynamicObject dynamicObject;

        [SetUp]
        public void SetUp()
        {
            dynamicObject = new DynamicObject();
            statePatternDyamicObject = new DynamicObjectStateManager();
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
    }
}

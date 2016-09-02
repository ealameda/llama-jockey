using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using Stateless;

namespace NUnit.Tests
{
    [TestFixture]
    class UnmagnetizedStateTest
    {
        UnmagnetizedState unmagnetizedState;
        DynamicObjectStatePatternManager dynamicObjectStatePatternManager;

        [SetUp]
        public void SetUp()
        {
            dynamicObjectStatePatternManager = new DynamicObjectStatePatternManager();
            unmagnetizedState = new UnmagnetizedState(dynamicObjectStatePatternManager, 0.0f);
            dynamicObjectStatePatternManager.Awake();
        }

        [Test]
        public void ShouldCallMagnetizedStateTransitionWhenInProximity()
        {
            unmagnetizedState.UpdateState();
            
        }
        //this will go in a different test class testing unmagnetized logic at unit level
        // make new Right pinch detector named "PinchDetector_R" and set it's posistion
        //unmagnatizedState = new UnmagnetizedState();
    }
}

using UnityEngine;
using System.Collections;

public class MagnetizedState : IDynamicObjectState
{
    private readonly StatePatternDynamicObject dynamicObject;

    public MagnetizedState(StatePatternDynamicObject dynamicObject)
    {
        this.dynamicObject = dynamicObject;
    }

    public void ToMagnetizedState() { }
    public void ToUnmagnetizedState()
    {
        dynamicObject.currentState = dynamicObject.unmagnetizedState;
    }
    public void ToGrabbedState() { }
    public void ToUngrabbedState() { }
    public void ToIntertOnPathState() { }
    public void ToMovingOnPathState() { }
    public void ToPausedOnPathState() { }
    public void OnEnable() { }
    public void OnDisable() { }
    public void UpdateState() { }
}

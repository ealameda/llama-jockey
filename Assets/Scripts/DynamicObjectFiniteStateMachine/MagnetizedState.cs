using UnityEngine;
using System.Collections;

public class MagnetizedState : IDynamicObjectState
{
    private readonly DynamicObjectStatePatternManager dynamicObject;

    public MagnetizedState(DynamicObjectStatePatternManager dynamicObject)
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

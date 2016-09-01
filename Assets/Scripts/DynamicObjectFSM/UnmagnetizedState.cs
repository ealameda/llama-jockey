using UnityEngine;
using System.Collections;

public class UnmagnetizedState : IDynamicObjectState
{
    private readonly StatePatternDynamicObject dynamicObject;
    private float magnetStartDistance;

    public UnmagnetizedState(StatePatternDynamicObject dynamicObject, float magnetStartDistance)
    {
        this.dynamicObject = dynamicObject;
        this.magnetStartDistance = magnetStartDistance;
    }

    public void ToMagnetizedState()
    {
        dynamicObject.currentState = dynamicObject.magnetizedState;
    }
    public void ToUnmagnetizedState()
    {
        Debug.LogError("Cannot move from Unmagnetized State to Unmagnetized State");
    }
    public void ToGrabbedState() { }
    public void ToUngrabbedState() { }
    public void ToIntertOnPathState() { }
    public void ToMovingOnPathState() { }
    public void ToPausedOnPathState() { }
    public void OnEnable() { }
    public void OnDisable() { }
    public void UpdateState()
    {
        Transform rightPinchDetector = GameObject.Find("PinchDetector_R").transform;
        if (rightPinchDetector != null
            && IsInProximity(rightPinchDetector.position, dynamicObject.transform.position, magnetStartDistance))
        {
            ToMagnetizedState();
        }
    }

    private bool IsInProximity(Vector3 handPosition, Vector3 dynamicObjectPosition, float distance)
    {
        if (Vector3.Distance(handPosition, dynamicObjectPosition) < distance)
        {
            return true;
        }
        return false;
    }
}

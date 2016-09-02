using UnityEngine;
using System.Collections;

public class UnmagnetizedState : IDynamicObjectState
{
    private readonly DynamicObjectStatePatternManager statePatternManager;
    private float magnetStartDistance;

    public UnmagnetizedState(DynamicObjectStatePatternManager statePatternManager, float magnetStartDistance)
    {
        this.statePatternManager = statePatternManager;
        this.magnetStartDistance = magnetStartDistance;
    }

    private void ToMagnetizedState()
    {
        statePatternManager.currentState = statePatternManager.magnetizedState;
    }
    private void ToUnmagnetizedState()
    {
        Debug.LogError("Cannot move from Unmagnetized State to Unmagnetized State");
    }
    public void OnEnable() { }
    public void OnDisable() { }
    public void UpdateState()
    {
        Transform rightPinchDetector = GameObject.Find("PinchDetector_R").transform;
        if (rightPinchDetector != null
            && IsInProximity(rightPinchDetector.position, statePatternManager.transform.position, magnetStartDistance))
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

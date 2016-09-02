using UnityEngine;
using System.Collections;
using Stateless;
using Assets.Scripts.DynamicObjectFiniteStateMachine;

public class UnmagnetizedState : State
{
    private readonly DynamicObjectStateManager stateMachine;
    private float magnetStartDistance;

    public UnmagnetizedState(float magnetStartDistance, StateMachine)
    {
        this.magnetStartDistance = magnetStartDistance;
    }

    public void OnEnable() { }
    public void OnDisable() { }
    public void UpdateState()
    {
        Transform rightPinchDetector = GameObject.Find("PinchDetector_R").transform;
        if (rightPinchDetector != null
            && IsInProximity(rightPinchDetector.position, stateMachine.transform.position, magnetStartDistance))
        {
            ToMagnetizedState();
        }
    }

    private void ToMagnetizedState()
    {
        stateMachine.Fire(Trigger.InProximity);
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
